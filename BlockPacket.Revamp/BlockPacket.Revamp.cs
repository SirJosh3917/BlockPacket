namespace SirJosh3917.RevampedBlockPacket
{
	using BlockTypes;
	using MessageTypes;
	using PlayerIOClient;

	public static class BlockPacket
	{
		public static bool TryDeserialize(Message msg, out Block block)
		{
			switch (msg.Type)
			{
				case "b":	return GruntWork<B, SimpleBlock>		.TryDeserialize(msg, out block);
				case "bn":	return GruntWork<Bn, NpcBlock>			.TryDeserialize(msg, out block);
				case "bc":	return GruntWork<Bc, NumberBlock>		.TryDeserialize(msg, out block);
				case "br":	return GruntWork<Br, MorphableBlock>	.TryDeserialize(msg, out block);
				case "bs":	return GruntWork<Bs, MusicBlock>		.TryDeserialize(msg, out block);
				case "lb":	return GruntWork<Lb, LabelBlock>		.TryDeserialize(msg, out block);
				case "pt":	return GruntWork<Pt, PortalBlock>		.TryDeserialize(msg, out block);
				case "ts":	return GruntWork<Ts, SignBlock>			.TryDeserialize(msg, out block);
				case "wp":	return GruntWork<Wp, WorldPortalBlock>	.TryDeserialize(msg, out block);

				default: return Helpers.TryEndMethod(false, default, out block);
			}
		}

		public static Message Serialize<T>(T block)
			where T : Block
		{
			switch (block)
			{
				case SimpleBlock b:			return GruntWork<SB, SimpleBlock>		.Serialize(b);
				case NpcBlock bn:			return GruntWork<SBn, NpcBlock>			.Serialize(bn);
				case NumberBlock bc:		return GruntWork<SBc, NumberBlock>		.Serialize(bc);
				case MorphableBlock br:		return GruntWork<SBr, MorphableBlock>	.Serialize(br);
				case MusicBlock bs:			return GruntWork<SBs, MusicBlock>		.Serialize(bs);
				case LabelBlock lb:			return GruntWork<SLb, LabelBlock>		.Serialize(lb);
				case PortalBlock pt:		return GruntWork<SPt, PortalBlock>		.Serialize(pt);
				case SignBlock ts:			return GruntWork<STs, SignBlock>		.Serialize(ts);
				case WorldPortalBlock wp:	return GruntWork<SWp, WorldPortalBlock>	.Serialize(wp);

				default: return default;
			}
		}
	}
	
	public static class GruntWork<TPacket, TBlock>
			where TPacket : class, IMessage
			where TBlock : Block, IPacketLink<TPacket>, new()
	{
		public static Message Serialize(TBlock block)
		{
			// i don't like the hardcoding of the 'b', but /shrug
			return Message.Create("b", Decorator.Serializer.SerializeItem<TPacket>(block.GetAs()).Arguments);
		}

		public static bool TryDeserialize(Message msg, out Block result)
			=> Helpers.TryEndMethod(TryDeserializeT(msg, out var des), des, out result);

		public static bool TryDeserializeT(Message msg, out TBlock result)
		{
			if (!Decorator.Deserializer.TryDeserializeItem<TPacket>(new Decorator.BasicMessage(msg.Type, msg.GetObjects()), out var desPacket))
				return Helpers.TryEndMethod(false, default, out result);

			var block = new TBlock();
			block.SetTo(desPacket);

			return Helpers.TryEndMethod(true, block, out result);
		}
	}

	internal static class Helpers
	{
		public static object[] GetObjects(this Message msg)
		{
			var objs = new object[msg.Count];

			if (objs.Length == 0) return objs;

			for (uint i = 0; i < msg.Count; i++)
				objs[i] = msg[i];

			return objs;
		}

		public static bool TryEndMethod<T>(bool @return, T result, out T @out)
		{
			@out = result;
			return @return;
		}
	}
}

namespace SirJosh3917.RevampedBlockPacket.BlockTypes
{
	using System;
	using MessageTypes;

	public abstract class Block
	{
		protected Block() { }

		protected Block(uint layer, uint x, uint y, uint playerId, uint blockId)
			=> Set(layer, x, y, playerId, blockId);

		protected void Set(uint layer, uint x, uint y, uint playerId, uint blockId)
		{
			Layer = layer;
			X = x;
			Y = y;
			PlayerId = playerId;
			BlockId = blockId;
		}

		public uint Layer { get; protected set; }
		public uint X { get; protected set; }
		public uint Y { get; protected set; }
		public uint PlayerId { get; protected set; }
		public uint BlockId { get; protected set; }
	}

	public interface IPacketLink<TPacket>
		where TPacket : IMessage
	{
		void SetTo(TPacket instance);
		TPacket GetAs();
	}

	public interface IRotatable
	{
		uint Rotation { get; }
	}

	public interface ITextInformation
	{
		string Text { get; }
	}

	public interface ITargettable<T>
	{
		T Target { get; }
	}

	public class SimpleBlock : Block, IPacketLink<B>, IPacketLink<SB>
	{
		public SimpleBlock() : base() { }

		public SimpleBlock(uint layer, uint x, uint y, uint playerId, uint blockId) : base(layer, x, y, playerId, blockId) { }

		public void SetTo(B b)
			=> Set((uint)b.Layer, b.X, b.Y, b.PlayerId, b.BlockId);

		B IPacketLink<B>.GetAs()
			=> new B {
				Layer = (int)Layer,
				X = X,
				Y = Y,
				BlockId = BlockId,
				PlayerId = PlayerId,
			};

		public void SetTo(SB instance) => throw new NotImplementedException();

		SB IPacketLink<SB>.GetAs()
			=> new SB {
				Layer = (int)Layer,
				X = (int)X,
				Y = (int)Y,
				BlockId = (int)BlockId,
			};
	}

	public class NpcBlock : Block, IPacketLink<Bn>, IPacketLink<SBn>
	{
		public NpcBlock() : base() { }

		public NpcBlock(uint layer, uint x, uint y, uint playerId, uint blockId, string npcName, string chat1, string chat2, string chat3) : base(layer, x, y, playerId, blockId)
		{
			NpcName = npcName;
			ChatMessages = GetChats(chat1, chat2, chat3);
		}

		internal NpcBlock(Bn b) : base(0, b.X, b.Y, b.PlayerId, b.BlockId)
		{
			NpcName = b.NpcName;
			ChatMessages = GetChats(b);
		}

		public void SetTo(Bn b)
		{
			Set(0, b.X, b.Y, b.PlayerId, b.BlockId);

			NpcName = b.NpcName;
			ChatMessages = GetChats(b);
		}

		public Bn GetAs()
			=> new Bn {
				X = X,
				Y = Y,
				BlockId = BlockId,
				NpcName = NpcName,
				Chat1 = ChatMessages[0],
				Chat2 = ChatMessages[1],
				Chat3 = ChatMessages[2],
				PlayerId = PlayerId
			};

		public void SetTo(SBn instance) => throw new NotImplementedException();

		SBn IPacketLink<SBn>.GetAs()
			=> new SBn {
				Layer = (int)Layer,
				X = (int)X,
				Y = (int)Y,
				BlockId = (int)BlockId,

				Chat1 = ChatMessages[0],
				Chat2 = ChatMessages[1],
				Chat3 = ChatMessages[2],
				NpcName = NpcName,
			};

		private static string[] GetChats(Bn b)
			=> GetChats(b.Chat1, b.Chat2, b.Chat3);

		private static string[] GetChats(string chat1, string chat2, string chat3)
		{
			var chats = new string[3];

			// :nauseated_face:
			var item = 0;

			chats[item++] = chat1 ?? "";
			chats[item++] = chat2 ?? "";
			chats[item++] = chat3 ?? "";

			return chats;
		}

		public string NpcName { get; protected set; }
		public string[] ChatMessages { get; protected set; }
	}

	public class NumberBlock : Block, IPacketLink<Bc>, IPacketLink<SBc>
	{
		public NumberBlock() : base() { }

		public NumberBlock(uint layer, uint x, uint y, uint playerId, uint blockId, uint numberValue) : base(layer, x, y, playerId, blockId)
			=> NumberValue = numberValue;

		internal NumberBlock(Bc b) : base(0, b.X, b.Y, b.PlayerId, b.BlockId) => NumberValue = b.NumberValue;

		public void SetTo(Bc b)
		{
			Set(0, b.X, b.Y, b.PlayerId, b.BlockId);

			NumberValue = b.NumberValue;
		}

		public Bc GetAs()
			=> new Bc {
				X = X,
				Y = Y,
				BlockId = BlockId,
				NumberValue = NumberValue,
				PlayerId = PlayerId,
			};

		public void SetTo(SBc instance) => throw new NotImplementedException();

		SBc IPacketLink<SBc>.GetAs()
			=> new SBc {
				Layer = (int)Layer,
				X = (int)X,
				Y = (int)Y,
				BlockId = (int)BlockId,

				NumberValue = (int)NumberValue,
			};

		public uint NumberValue { get; protected set; }
	}

	public class MorphableBlock : Block, IPacketLink<Br>, IPacketLink<SBr>
	{
		public MorphableBlock() : base() { }

		public MorphableBlock(uint layer, uint x, uint y, uint playerId, uint blockId, uint numberValue) : base(layer, x, y, playerId, blockId)
			=> Morph = numberValue;

		internal MorphableBlock(Br b) : base(0, b.X, b.Y, b.PlayerId, b.BlockId) => Morph = b.Morph;

		public void SetTo(Br b)
		{
			Set((uint)b.Layer, b.X, b.Y, b.PlayerId, b.BlockId);

			Morph = b.Morph;
		}

		public Br GetAs()
			=> new Br {
				X = X,
				Y = Y,
				BlockId = BlockId,
				Morph = Morph,
				Layer = (int)Layer,
				PlayerId = PlayerId,
			};

		public void SetTo(SBr instance) => throw new NotImplementedException();

		SBr IPacketLink<SBr>.GetAs()
			=> new SBr {
				Layer = (int)Layer,
				X = (int)X,
				Y = (int)Y,
				BlockId = (int)BlockId,

				Morphable = (int)Morph,
			};

		public uint Morph { get; protected set; }
	}

	public class MusicBlock : Block, IPacketLink<Bs>, IPacketLink<SBs>
	{
		public MusicBlock() : base() { }

		public MusicBlock(uint layer, uint x, uint y, uint playerId, uint blockId, int sound) : base(layer, x, y, playerId, blockId) => Sound = sound;

		internal MusicBlock(Bs b) : base(0, b.X, b.Y, b.PlayerId, b.BlockId) => Sound = b.Sound;

		public void SetTo(Bs b)
		{
			Set(0, b.X, b.Y, b.PlayerId, b.BlockId);

			Sound = b.Sound;
		}

		public Bs GetAs()
			=> new Bs {
				X = X,
				Y = Y,
				BlockId = BlockId,
				Sound = Sound,
				PlayerId = PlayerId,
			};

		public void SetTo(SBs instance) => throw new NotImplementedException();

		SBs IPacketLink<SBs>.GetAs()
			=> new SBs {
				Layer = (int)Layer,
				X = (int)X,
				Y = (int)Y,
				BlockId = (int)BlockId,

				SoundValue = Sound,
			};

		public int Sound { get; protected set; }
	}

	public class LabelBlock : Block, ITextInformation, IPacketLink<Lb>, IPacketLink<SLb>
	{
		private const uint ID = 1000;

		public LabelBlock() : base() { }

		public LabelBlock(uint layer, uint x, uint y, uint playerId, uint blockId, string text, string textColor) : base(layer, x, y, playerId, blockId)
		{
			Text = text;
			TextColor = textColor;
		}

		internal LabelBlock(Lb b) : base(0, b.X, b.Y, b.PlayerId, (uint)b.BlockId)
		{
			Text = b.Text;
			TextColor = b.TextColor;
		}

		public void SetTo(Lb b)
		{
			Set(0, b.X, b.Y, b.PlayerId, (uint)b.BlockId);

			Text = b.Text;
			TextColor = b.TextColor;
		}

		public Lb GetAs()
			=> new Lb {
				X = X,
				Y = Y,
				BlockId = (int)BlockId,
				Text = Text,
				TextColor = TextColor,
			};

		public void SetTo(SLb instance) => throw new NotImplementedException();

		SLb IPacketLink<SLb>.GetAs()
			=> new SLb {
				Layer = (int)Layer,
				X = (int)X,
				Y = (int)Y,
				BlockId = (int)BlockId,

				LabelText = Text,
				LabelColor = TextColor,
			};

		public string Text { get; protected set; }
		public string TextColor { get; protected set; }
	}

	public class PortalBlock : Block, IRotatable, ITargettable<uint>, IPacketLink<Pt>, IPacketLink<SPt>
	{
		public PortalBlock() : base() { }

		public PortalBlock(uint layer, uint x, uint y, uint playerId, uint blockId, uint rotation, uint id, uint target) : base(layer, x, y, playerId, blockId)
		{
			Rotation = rotation;
			Id = id;
			Target = target;
		}

		internal PortalBlock(Pt b) : base(0, b.X, b.Y, b.PlayerId, b.BlockId)
		{
			Rotation = b.PortalRotation;
			Id = b.PortalId;
			Target = b.PortalTarget;
		}

		public void SetTo(Pt b)
		{
			Set(0, b.X, b.Y, b.PlayerId, b.BlockId);

			Rotation = b.PortalRotation;
			Id = b.PortalId;
			Target = b.PortalTarget;
		}

		public Pt GetAs()
			=> new Pt {
				X = X,
				Y = Y,
				BlockId = BlockId,
				PortalRotation = Rotation,
				PortalId = Id,
				PortalTarget = Target,
				PlayerId = PlayerId,
			};

		public void SetTo(SPt instance) => throw new NotImplementedException();

		SPt IPacketLink<SPt>.GetAs()
			=> new SPt {
				Layer = (int)Layer,
				X = (int)X,
				Y = (int)Y,
				BlockId = (int)BlockId,

				Rotation = (int)Rotation,
				Id = (int)Id,
				Target = (int)Target,
			};

		public uint Rotation { get; protected set; }
		public uint Id { get; protected set; }
		public uint Target { get; protected set; }
	}

	public class SignBlock : Block, ITextInformation, IPacketLink<Ts>, IPacketLink<STs>
	{
		public const uint ID = 385;

		//TODO: investigate signblock having an id

		public SignBlock() : base() { }

		public SignBlock(uint layer, uint x, uint y, uint playerId, uint blockId, string text, uint type) : base(layer, x, y, playerId, blockId)
		{
			Text = text;
			Type = type;
		}

		internal SignBlock(Ts b) : base(0, b.X, b.Y, b.PlayerId, b.BlockId)
		{
			Text = b.Text;
			Type = b.SignType;
		}

		public void SetTo(Ts b)
		{
			Set(0, b.X, b.Y, b.PlayerId, b.BlockId);

			Text = b.Text;
			Type = b.SignType;
		}

		public Ts GetAs()
			=> new Ts {
				X = X,
				Y = Y,
				BlockId = BlockId,
				Text = Text,
				SignType = Type,
				PlayerId = PlayerId,
			};

		public void SetTo(STs instance) => throw new NotImplementedException();

		STs IPacketLink<STs>.GetAs()
			=> new STs {
				Layer = (int)Layer,
				X = (int)X,
				Y = (int)Y,
				BlockId = (int)BlockId,

				SignText = Text,
				SignType = (int)Type,
			};

		public string Text { get; protected set; }
		public uint Type { get; protected set; }
	}

	public class WorldPortalBlock : Block, ITargettable<string>, IPacketLink<Wp>, IPacketLink<SWp>
	{
		public const uint ID = 374;

		//TODO: this has a world id too? no way

		public WorldPortalBlock() : base() { }

		public WorldPortalBlock(uint layer, uint x, uint y, uint playerId, uint blockId, string target) : base(layer, x, y, playerId, blockId) => Target = target;

		internal WorldPortalBlock(Wp b) : base(0, b.X, b.Y, b.PlayerId, ID) => Target = b.Target;

		public void SetTo(Wp b)
		{
			Set(0, b.X, b.Y, b.PlayerId, ID);

			Target = b.Target;
		}

		public Wp GetAs()
			=> new Wp {
				X = X,
				Y = Y,
				BlockId = BlockId,
				Target = Target,
			};

		public void SetTo(SWp instance) => throw new NotImplementedException();

		SWp IPacketLink<SWp>.GetAs()
			=> new SWp {
				Layer = (int)Layer,
				X = (int)X,
				Y = (int)Y,
				BlockId = (int)BlockId,

				Target = Target,
			};

		public string Target { get; protected set; }
	}
}

namespace SirJosh3917.RevampedBlockPacket.MessageTypes
{
	using Decorator.Attributes;

	public interface IMessage
	{

	}

	#region receivables
	public class RBTemplate
	{
		[Position(0), Required]
		public uint X { get; set; }

		[Position(1), Required]
		public uint Y { get; set; }

		[Position(2), Required]
		public uint BlockId { get; set; }
	}

	[Message("b")]
	public class B : IMessage
	{
		[Position(0), Required]
		public int Layer { get; set; }

		[Position(1), Required]
		public uint X { get; set; }

		[Position(2), Required]
		public uint Y { get; set; }

		[Position(3), Required]
		public uint BlockId { get; set; }

		[Position(4), Required]
		public uint PlayerId { get; set; }
	}

	[Message("bn")]
	public class Bn : RBTemplate, IMessage
	{
		[Position(3), Required]
		public string NpcName { get; set; }

		[Position(4), Required]
		public string Chat1 { get; set; }

		[Position(5), Required]
		public string Chat2 { get; set; }

		[Position(6), Required]
		public string Chat3 { get; set; }

		[Position(7), Required]
		public uint PlayerId { get; set; }
	}

	[Message("bc")]
	public class Bc : RBTemplate, IMessage
	{
		[Position(3), Required]
		public uint NumberValue { get; set; }

		[Position(4), Required]
		public uint PlayerId { get; set; }
	}

	[Message("br")]
	public class Br : RBTemplate, IMessage
	{
		[Position(3), Required]
		public uint Morph { get; set; }

		[Position(4), Required]
		public int Layer { get; set; }

		[Position(5), Required]
		public uint PlayerId { get; set; }
	}

	[Message("bs")]
	public class Bs : RBTemplate, IMessage
	{
		[Position(3), Required]
		public int Sound { get; set; }

		[Position(4), Required]
		public uint PlayerId { get; set; }
	}

	[Message("lb")]
	public class Lb : IMessage
	{
		[Position(0), Required]
		public uint X { get; set; }

		[Position(1), Required]
		public uint Y { get; set; }

		[Position(2), Required]
		public int BlockId { get; set; }

		[Position(3), Required]
		public string Text { get; set; }

		[Position(4), Required]
		public string TextColor { get; set; }

		[Position(5), Required]
		public uint PlayerId { get; set; }
	}

	[Message("pt")]
	public class Pt : RBTemplate, IMessage
	{
		[Position(3), Required]
		public uint PortalRotation { get; set; }

		[Position(4), Required]
		public uint PortalId { get; set; }

		[Position(5), Required]
		public uint PortalTarget { get; set; }

		[Position(6), Required]
		public uint PlayerId { get; set; }
	}

	[Message("ts")]
	public class Ts : RBTemplate, IMessage
	{
		[Position(3), Required]
		public string Text { get; set; }

		[Position(4), Required]
		public uint SignType { get; set; }

		[Position(5), Required]
		public uint PlayerId { get; set; }
	}

	[Message("wp")]
	public class Wp : RBTemplate, IMessage
	{
		[Position(3), Required]
		public string Target { get; set; }

		[Position(4), Required]
		public uint PlayerId { get; set; }
	}
	#endregion

	#region sendables
	[Message("b")]
	public abstract class Sendable : IMessage
	{
		[Position(0), Required]
		public int Layer { get; set; }

		[Position(1), Required]
		public int X { get; set; }

		[Position(2), Required]
		public int Y { get; set; }

		[Position(3), Required]
		public int BlockId { get; set; }
	}

	public class SB : Sendable
	{

	}

	public class SBc : Sendable
	{
		[Position(4), Required]
		public int NumberValue { get; set; }
	}

	public class SBr : Sendable
	{
		[Position(4), Required]
		public int Morphable { get; set; }
	}

	public class SBn : Sendable
	{
		[Position(4), Required]
		public string NpcName { get; set; }

		[Position(5), Required]
		public string Chat1 { get; set; }

		[Position(6), Required]
		public string Chat2 { get; set; }

		[Position(7), Required]
		public string Chat3 { get; set; }
	}

	public class SBs : Sendable
	{
		[Position(4), Required]
		public int SoundValue { get; set; }
	}

	public class SLb : Sendable
	{
		[Position(4), Required]
		public string LabelText { get; set; }

		[Position(5), Required]
		public string LabelColor { get; set; }
	}

	public class SPt : Sendable
	{
		[Position(4), Required]
		public int Rotation { get; set; }

		[Position(5), Required]
		public int Id { get; set; }

		[Position(6), Required]
		public int Target { get; set; }
	}

	public class STs : Sendable
	{
		[Position(4), Required]
		public string SignText { get; set; }

		[Position(5), Required]
		public int SignType { get; set; }
	}

	public class SWp : Sendable
	{
		[Position(4), Required]
		public string Target { get; set; }
	}
	#endregion
}
using PlayerIOClient;
using SirJosh3917.RevampedBlockPacket;
using SirJosh3917.RevampedBlockPacket.BlockTypes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SirJosh3917.RevampedBlockPacket.Tests
{
	public class BlockPacketTests
	{
		public const uint Layer = 0;
		public const int IntLayer = 0;
		public const uint X = 9;
		public const uint Y = 11;
		public const uint PID = 8;

		public const int IntValue = 5;
		public const uint AdditionalValue = 2;
		public const string DummyText = "this be some dummy text";

		public const uint Plain = 9;

		public const uint NumberValue = 43; //coindoor
		public const uint NumberValueValue = AdditionalValue;

		public const uint NPCId = 0;
		public const string NPCName = "admin";
		public const string NPCChat1 = DummyText;
		public const string NPCChat2 = "more dummy text";
		public const string NPCChat3 = "egg-(e v e n) more dummy text";

		public const uint Morphable = 1052; //morphable gray door thing
		public const uint MorphableValue = AdditionalValue;

		public const uint Sound = 77;
		public const int SoundValue = IntValue;

		public const int Label = 1000; //label id
		public const string LabelText = DummyText;
		public const string LabelColor = "#00ff00";

		public const uint Portal = 242;
		public const uint PortalRotation = AdditionalValue;
		public const uint PortalId = 13;
		public const uint PortalTarget = 37;

		public const uint SignId = 385;
		public const string SignText = DummyText;
		public const uint SignType = AdditionalValue;

		public const uint WorldPortalId = 374;
		public const string WorldTarget = "PWSpriteTesting";

		public static readonly Message b = Message.Create("b", IntLayer, X, Y, Plain, PID);
		public static readonly Message bc = Message.Create("bc", X, Y, NumberValue, NumberValueValue, PID);
		public static readonly Message bn = Message.Create("bn", X, Y, NPCId, NPCName, NPCChat1, NPCChat2, NPCChat3, PID);
		public static readonly Message br = Message.Create("br", X, Y, Morphable, MorphableValue, IntLayer, PID);
		public static readonly Message bs = Message.Create("bs", X, Y, Sound, SoundValue, PID);
		public static readonly Message lb = Message.Create("lb", X, Y, Label, LabelText, LabelColor, PID);
		public static readonly Message pt = Message.Create("pt", X, Y, Portal, PortalRotation, PortalId, PortalTarget, PID);
		public static readonly Message ts = Message.Create("ts", X, Y, SignId, SignText, SignType, PID);
		public static readonly Message wp = Message.Create("wp", X, Y, WorldPortalId, WorldTarget, PID);

		public static readonly Message placeb = Message.Create("b", IntLayer, (int)X, (int)Y, (int)Plain);
		public static readonly Message placebc = Message.Create("b", IntLayer, (int)X, (int)Y, (int)NumberValue, (int)NumberValueValue);
		public static readonly Message placebn = Message.Create("b", IntLayer, (int)X, (int)Y, (int)NPCId, NPCName, NPCChat1, NPCChat2, NPCChat3);
		public static readonly Message placebr = Message.Create("b", IntLayer, (int)X, (int)Y, (int)Morphable, (int)MorphableValue);
		public static readonly Message placebs = Message.Create("b", IntLayer, (int)X, (int)Y, (int)Sound, SoundValue);
		public static readonly Message placelb = Message.Create("b", IntLayer, (int)X, (int)Y, Label, LabelText, LabelColor);
		public static readonly Message placept = Message.Create("b", IntLayer, (int)X, (int)Y, (int)Portal, (int)PortalRotation, (int)PortalId, (int)PortalTarget);
		public static readonly Message placets = Message.Create("b", IntLayer, (int)X, (int)Y, (int)SignId, SignText, (int)SignType);
		public static readonly Message placewp = Message.Create("b", IntLayer, (int)X, (int)Y, (int)WorldPortalId, WorldTarget);

		#region valid
		private void Validate(Message msg, string item) => Assert.True(BlockPacket.TryDeserialize(msg, out _), $"'{item}' is found to be invalid.");

		[Fact]
		public void ValidB() => Validate(b, nameof(b));

		[Fact]
		public void ValidBC() => Validate(bc, nameof(bc));

		[Fact]
		public void ValidBN() => Validate(bn, nameof(bn));

		[Fact]
		public void ValidBR() => Validate(br, nameof(br));

		[Fact]
		public void ValidBS() => Validate(bs, nameof(bs));

		[Fact]
		public void ValidLB() => Validate(lb, nameof(lb));

		[Fact]
		public void ValidPT() => Validate(pt, nameof(pt));

		[Fact]
		public void ValidTS() => Validate(ts, nameof(ts));

		[Fact]
		public void ValidWP() => Validate(wp, nameof(wp));
		#endregion

		#region deserialize
		private T TryDes<T>(Message m)
			where T : Block
		{
			if (!BlockPacket.TryDeserialize(m, out var bp)) Assert.False(true, "Unable to deserialize.");

			if (bp is T)
			{
				return (T)bp;
			}
			else Assert.False(true, $"Should be a {nameof(T)}");

			throw new NotImplementedException();
		}

		[Fact]
		public void DeserializesB()
		{
			var block = b;

			var itm = TryDes<SimpleBlock>(block);

			Assert.Equal(block[0], (int)itm.Layer);
			Assert.Equal(block[1], itm.X);
			Assert.Equal(block[2], itm.Y);
			Assert.Equal(block[3], itm.BlockId);
			Assert.Equal(block[4], itm.PlayerId);
		}

		[Fact]
		public void DeserializesBN()
		{
			var block = bn;

			var itm = TryDes<NpcBlock>(block);

			Assert.Equal(block[0], itm.X);
			Assert.Equal(block[1], itm.Y);
			Assert.Equal(block[2], itm.BlockId);
			Assert.Equal(block[3], itm.NpcName);
			Assert.Equal(block[4], itm.ChatMessages[0]);
			Assert.Equal(block[5], itm.ChatMessages[1]);
			Assert.Equal(block[6], itm.ChatMessages[2]);
		}

		[Fact]
		public void DeserializesBC()
		{
			var block = bc;

			var itm = TryDes<NumberBlock>(block);

			Assert.Equal(block[0], itm.X);
			Assert.Equal(block[1], itm.Y);
			Assert.Equal(block[2], itm.BlockId);
			Assert.Equal(block[3], itm.NumberValue);
			Assert.Equal(block[4], itm.PlayerId);
		}

		[Fact]
		public void DeserializesBR()
		{
			var block = br;

			var itm = TryDes<MorphableBlock>(block);

			Assert.Equal(block[0], itm.X);
			Assert.Equal(block[1], itm.Y);
			Assert.Equal(block[2], itm.BlockId);
			Assert.Equal(block[3], itm.Morph);
			Assert.Equal(block[4], (int)itm.Layer);
			Assert.Equal(block[5], itm.PlayerId);
		}

		[Fact]
		public void DeserializesBS()
		{
			var block = bs;

			var itm = TryDes<MusicBlock>(block);

			Assert.Equal(block[0], itm.X);
			Assert.Equal(block[1], itm.Y);
			Assert.Equal(block[2], itm.BlockId);
			Assert.Equal(block[3], itm.Sound);
			Assert.Equal(block[4], itm.PlayerId);
		}

		[Fact]
		public void DeserializesLB()
		{
			var block = lb;

			var itm = TryDes<LabelBlock>(block);

			Assert.Equal(block[0], itm.X);
			Assert.Equal(block[1], itm.Y);
			Assert.Equal(block[2], (int)itm.BlockId);
			Assert.Equal(block[3], itm.Text);
			Assert.Equal(block[4], itm.TextColor);
			Assert.Equal(block[5], itm.PlayerId);
		}

		[Fact]
		public void DeserializesPT()
		{
			var block = pt;

			var itm = TryDes<PortalBlock>(block);

			Assert.Equal(block[0], itm.X);
			Assert.Equal(block[1], itm.Y);
			Assert.Equal(block[2], itm.BlockId);
			Assert.Equal(block[3], itm.Rotation);
			Assert.Equal(block[4], itm.Id);
			Assert.Equal(block[5], itm.Target);
			Assert.Equal(block[6], itm.PlayerId);
		}

		[Fact]
		public void DeserializesTS()
		{
			var block = ts;

			var itm = TryDes<SignBlock>(block);

			Assert.Equal(block[0], itm.X);
			Assert.Equal(block[1], itm.Y);
			Assert.Equal(block[2], itm.BlockId);
			Assert.Equal(block[3], itm.Text);
			Assert.Equal(block[4], itm.Type);
			Assert.Equal(block[5], itm.PlayerId);
		}

		[Fact]
		public void DeserializesWP()
		{
			var block = wp;

			var itm = TryDes<WorldPortalBlock>(block);

			Assert.Equal(block[0], itm.X);
			Assert.Equal(block[1], itm.Y);
			Assert.Equal(block[2], itm.BlockId);
			Assert.Equal(block[3], itm.Target);
			Assert.Equal(block[4], itm.PlayerId);
		}
		#endregion

		#region serialize
		private Message TrySer<TDes>(Message m)
			where TDes : Block
		{
			if (!BlockPacket.TryDeserialize(m, out var bp)) Assert.False(true, "Unable to deserialize.");

			if (bp is TDes td)
			{
				return BlockPacket.Serialize<TDes>(td);
			}
			else Assert.False(true, $"Should be a {nameof(TDes)}");

			throw new NotImplementedException();
		}

		private void EnsureEquals(Message a, Message b)
		{
			Assert.Equal(a.Type, b.Type);
			Assert.Equal(a.Count, b.Count);

			for (uint i = 0; i < a.Count; i++)
				Assert.Equal(a[i], b[i]);
		}

		[Fact]
		public void SerializesB() => EnsureEquals(placeb, TrySer<SimpleBlock>(b));

		[Fact]
		public void SerializesBN() => EnsureEquals(placebn, TrySer<NpcBlock>(bn));

		[Fact]
		public void SerializesBC() => EnsureEquals(placebc, TrySer<NumberBlock>(bc));

		[Fact]
		public void SerializesBR() => EnsureEquals(placebr, TrySer<MorphableBlock>(br));

		[Fact]
		public void SerializesBS() => EnsureEquals(placebs, TrySer<MusicBlock>(bs));

		[Fact]
		public void SerializesLB() => EnsureEquals(placelb, TrySer<LabelBlock>(lb));

		[Fact]
		public void SerializesPT() => EnsureEquals(placept, TrySer<PortalBlock>(pt));

		[Fact]
		public void SerializesTS() => EnsureEquals(placets, TrySer<SignBlock>(ts));

		[Fact]
		public void SerializesWP() => EnsureEquals(placewp, TrySer<WorldPortalBlock>(wp));
		#endregion
	}
}
using PlayerIOClient;
using SirJosh3917;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Categories;

namespace BlockPacket.Tests {
	public class BlockPacketTests {
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

		public static readonly Message block_b = Message.Create("block", PID, Layer, X, Y, Plain, (uint)BlockPacket.BlockIdentifier.Plain);
		public static readonly Message block_bc = Message.Create("block", PID, Layer, X, Y, NumberValue, (uint)BlockPacket.BlockIdentifier.NumberValue, NumberValueValue);
		public static readonly Message block_bn = Message.Create("block", PID, Layer, X, Y, NPCId, (uint)BlockPacket.BlockIdentifier.NPC, NPCName, NPCChat1, NPCChat2, NPCChat3);
		public static readonly Message block_br = Message.Create("block", PID, Layer, X, Y, Morphable, (uint)BlockPacket.BlockIdentifier.Morphable, MorphableValue);
		public static readonly Message block_bs = Message.Create("block", PID, Layer, X, Y, Sound, (uint)BlockPacket.BlockIdentifier.Sound, (uint)SoundValue);
		public static readonly Message block_lb = Message.Create("block", PID, Layer, X, Y, (uint)Label, (uint)BlockPacket.BlockIdentifier.Label, LabelText, LabelColor);
		public static readonly Message block_pt = Message.Create("block", PID, Layer, X, Y, Portal, (uint)BlockPacket.BlockIdentifier.Portal, PortalRotation, PortalId, PortalTarget);
		public static readonly Message block_ts = Message.Create("block", PID, Layer, X, Y, SignId, (uint)BlockPacket.BlockIdentifier.Sign, SignText, SignType);
		public static readonly Message block_wp = Message.Create("block", PID, Layer, X, Y, WorldPortalId, (uint)BlockPacket.BlockIdentifier.WorldPortal, WorldTarget);

		#region valid
		[Fact]
		[Category("BlockPacket")]
		public void ValidB() => Assert.True(BlockPacket.IsValidEEBlockMessage(b), "Found 'b' to be invalid.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBC() => Assert.True(BlockPacket.IsValidEEBlockMessage(bc), "Found 'bc' to be invalid.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBN() => Assert.True(BlockPacket.IsValidEEBlockMessage(bn), "Found 'bn' to be invalid.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBR() => Assert.True(BlockPacket.IsValidEEBlockMessage(br), "Found 'br' to be invalid.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBS() => Assert.True(BlockPacket.IsValidEEBlockMessage(bs), "Found 'bs' to be invalid.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidLB() => Assert.True(BlockPacket.IsValidEEBlockMessage(lb), "Found 'lb' to be invalid.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidPT() => Assert.True(BlockPacket.IsValidEEBlockMessage(pt), "Found 'pt' to be invalid.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidTS() => Assert.True(BlockPacket.IsValidEEBlockMessage(ts), "Found 'ts' to be invalid.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidWP() => Assert.True(BlockPacket.IsValidEEBlockMessage(wp), "Found 'wp' to be invalid.");
		#endregion

		#region deserialize
		[Fact]
		[Category("BlockPacket")]
		public void DeserializesB() {
			var bp = new BlockPacket(b);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Plain, "Plain");
			EnsureBasics(bp, true);
		}

		[Fact]
		[Category("BlockPacket")]
		public void DeserializesBC() {
			var bp = new BlockPacket(bc);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.NumberValue, "NumberValue");
			EnsureBasics(bp, true);
			Assert.True(bp.NumberValue == NumberValueValue, $"bp.NumberValue ({bp.NumberValue}) != NumberValueValue ({NumberValueValue})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void DeserializesBN() {
			var bp = new BlockPacket(bn);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.NPC, "NPC");
			EnsureBasics(bp, true);
			Assert.True(bp.NPCName == NPCName, $"bp.NPCName ({bp.NPCName}) != NPCName ({NPCName})");
			Assert.True(bp.NPCChat1 == NPCChat1, $"bp.NPCChat1 ({bp.NPCChat1}) != NPCChat1 ({NPCChat1})");
			Assert.True(bp.NPCChat2 == NPCChat2, $"bp.NPCChat2 ({bp.NPCChat2}) != NPCChat2 ({NPCChat2})");
			Assert.True(bp.NPCChat3 == NPCChat3, $"bp.NPCChat3 ({bp.NPCChat3}) != NPCChat3 ({NPCChat3})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void DeserializesBR() {
			var bp = new BlockPacket(br);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Morphable, "Morphable");
			EnsureBasics(bp, true);
			Assert.True(bp.MorphableValue == MorphableValue, $"bp.MorphableValue ({bp.MorphableValue}) != MorphableValue ({MorphableValue})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void DeserializesBS() {
			var bp = new BlockPacket(bs);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Sound, "Sound");
			EnsureBasics(bp, true);
			Assert.True(bp.SoundValue == SoundValue, $"bp.SoundValue ({bp.SoundValue}) != SoundValue ({SoundValue})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void DeserializesLB() {
			var bp = new BlockPacket(lb);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Label, "Label");
			EnsureBasics(bp, true);
			Assert.True(bp.LabelText == LabelText, $"bp.LabelText ({bp.LabelText}) != LabelText ({LabelText})");
			Assert.True(bp.LabelColor == LabelColor, $"bp.LabelColor ({bp.LabelColor}) != LabelColor ({LabelColor})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void DeserializesPT() {
			var bp = new BlockPacket(pt);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Portal, "Portal");
			EnsureBasics(bp, true);
			Assert.True(bp.PortalId == PortalId, $"bp.PortalId ({bp.PortalId}) != PortalId ({PortalId})");
			Assert.True(bp.PortalRotation == PortalRotation, $"bp.PortalRotation ({bp.PortalRotation}) != PortalRotation ({PortalRotation})");
			Assert.True(bp.PortalTarget == PortalTarget, $"bp.PortalTarget ({bp.PortalTarget}) != PortalTarget ({PortalTarget})");
			//Assert.True(bp. == , $"bp. ({bp.}) !=  ({})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void DeserializesTS() {
			var bp = new BlockPacket(ts);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Sign, "Sign");
			EnsureBasics(bp, true);
			Assert.True(bp.SignText == SignText, $"bp.SignText ({bp.SignText}) != SignText ({SignText})");
			Assert.True(bp.SignType == SignType, $"bp.SignType ({bp.SignType}) != SignType ({SignType})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void DeserializesWP() {
			var bp = new BlockPacket(wp);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.WorldPortal, "WorldPortal");
			EnsureBasics(bp, true);
			Assert.True(bp.WorldTarget == WorldTarget, $"bp.WorldTarget ({bp.WorldTarget}) != WorldTarget ({WorldTarget})");
		}
		#endregion

		#region equals
		[Fact]
		[Category("BlockPacket")]
		public void SerializesBtoB() => MessagesEqual(b, new BlockPacket(b).SerializeToB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesBCtoB() => MessagesEqual(bc, new BlockPacket(bc).SerializeToB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesBNtoB() => MessagesEqual(bn, new BlockPacket(bn).SerializeToB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesBRtoB() => MessagesEqual(br, new BlockPacket(br).SerializeToB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesBStoB() => MessagesEqual(bs, new BlockPacket(bs).SerializeToB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesLBtoB() => MessagesEqual(lb, new BlockPacket(lb).SerializeToB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesPTtoB() => MessagesEqual(pt, new BlockPacket(pt).SerializeToB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesTStoB() => MessagesEqual(ts, new BlockPacket(ts).SerializeToB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesWPtoB() => MessagesEqual(wp, new BlockPacket(wp).SerializeToB());
		#endregion

		#region sendable b
		[Fact]
		[Category("BlockPacket")]
		public void SerializesBtoSendableB() => MessagesEqual(placeb, new BlockPacket(b).SerializeToSendableB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesBCtoSendableB() => MessagesEqual(placebc, new BlockPacket(bc).SerializeToSendableB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesBNtoSendableB() => MessagesEqual(placebn, new BlockPacket(bn).SerializeToSendableB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesBRtoSendableB() => MessagesEqual(placebr, new BlockPacket(br).SerializeToSendableB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesBStoSendableB() => MessagesEqual(placebs, new BlockPacket(bs).SerializeToSendableB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesLBtoSendableB() => MessagesEqual(placelb, new BlockPacket(lb).SerializeToSendableB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesPTtoSendableB() => MessagesEqual(placept, new BlockPacket(pt).SerializeToSendableB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesTStoSendableB() => MessagesEqual(placets, new BlockPacket(ts).SerializeToSendableB());

		[Fact]
		[Category("BlockPacket")]
		public void SerializesWPtoSendableB() => MessagesEqual(placewp, new BlockPacket(wp).SerializeToSendableB());
		#endregion

		#region valid blockpackets
		[Fact]
		[Category("BlockPacket")]
		public void ValidBlockPacket_B() => Assert.True(BlockPacket.IsValidCustomBlockMessage(block_b), $"block_b was deemed an invalid custom block message.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBlockPacket_BC() => Assert.True(BlockPacket.IsValidCustomBlockMessage(block_bc), $"block_bc was deemed an invalid custom block message.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBlockPacket_BN() => Assert.True(BlockPacket.IsValidCustomBlockMessage(block_bn), $"block_bn was deemed an invalid custom block message.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBlockPacket_BR() => Assert.True(BlockPacket.IsValidCustomBlockMessage(block_br), $"block_br was deemed an invalid custom block message.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBlockPacket_BS() => Assert.True(BlockPacket.IsValidCustomBlockMessage(block_bs), $"block_bs was deemed an invalid custom block message.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBlockPacket_LB() => Assert.True(BlockPacket.IsValidCustomBlockMessage(block_lb), $"block_lb was deemed an invalid custom block message.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBlockPacket_PT() => Assert.True(BlockPacket.IsValidCustomBlockMessage(block_pt), $"block_pt was deemed an invalid custom block message.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBlockPacket_TS() => Assert.True(BlockPacket.IsValidCustomBlockMessage(block_ts), $"block_ts was deemed an invalid custom block message.");

		[Fact]
		[Category("BlockPacket")]
		public void ValidBlockPacket_WP() => Assert.True(BlockPacket.IsValidCustomBlockMessage(block_wp), $"block_wp was deemed an invalid custom block message.");
		#endregion

		#region verifying 'block' is fine
		[Fact]
		[Category("BlockPacket")]
		public void BlockPacket_B() {
			var bp = new BlockPacket(block_b);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Plain, "Plain");
			EnsureBasics(bp, true);
		}

		[Fact]
		[Category("BlockPacket")]
		public void BlockPacket_BC() {
			var bp = new BlockPacket(block_bc);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.NumberValue, "NumberValue");
			EnsureBasics(bp, true);
			Assert.True(bp.NumberValue == NumberValueValue, $"bp.NumberValue ({bp.NumberValue}) != NumberValueValue ({NumberValueValue})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void BlockPacket_BN() {
			var bp = new BlockPacket(block_bn);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.NPC, "NPC");
			EnsureBasics(bp, true);
			Assert.True(bp.NPCName == NPCName, $"bp.NPCName ({bp.NPCName}) != NPCName ({NPCName})");
			Assert.True(bp.NPCChat1 == NPCChat1, $"bp.NPCChat1 ({bp.NPCChat1}) != NPCChat1 ({NPCChat1})");
			Assert.True(bp.NPCChat2 == NPCChat2, $"bp.NPCChat2 ({bp.NPCChat2}) != NPCChat2 ({NPCChat2})");
			Assert.True(bp.NPCChat3 == NPCChat3, $"bp.NPCChat3 ({bp.NPCChat3}) != NPCChat3 ({NPCChat3})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void BlockPacket_BR() {
			var bp = new BlockPacket(block_br);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Morphable, "Morphable");
			EnsureBasics(bp, true);
			Assert.True(bp.MorphableValue == MorphableValue, $"bp.MorphableValue ({bp.MorphableValue}) != MorphableValue ({MorphableValue})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void BlockPacket_BS() {
			var bp = new BlockPacket(block_bs);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Sound, "Sound");
			EnsureBasics(bp, true);
			Assert.True(bp.SoundValue == SoundValue, $"bp.SoundValue ({bp.SoundValue}) != SoundValue ({SoundValue})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void BlockPacket_LB() {
			var bp = new BlockPacket(block_lb);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Label, "Label");
			EnsureBasics(bp, true);
			Assert.True(bp.LabelText == LabelText, $"bp.LabelText ({bp.LabelText}) != LabelText ({LabelText})");
			Assert.True(bp.LabelColor == LabelColor, $"bp.LabelColor ({bp.LabelColor}) != LabelColor ({LabelColor})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void BlockPacket_PT() {
			var bp = new BlockPacket(block_pt);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Portal, "Portal");
			EnsureBasics(bp, true);
			Assert.True(bp.PortalId == PortalId, $"bp.PortalId ({bp.PortalId}) != PortalId ({PortalId})");
			Assert.True(bp.PortalRotation == PortalRotation, $"bp.PortalRotation ({bp.PortalRotation}) != PortalRotation ({PortalRotation})");
			Assert.True(bp.PortalTarget == PortalTarget, $"bp.PortalTarget ({bp.PortalTarget}) != PortalTarget ({PortalTarget})");
			//Assert.True(bp. == , $"bp. ({bp.}) !=  ({})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void BlockPacket_TS() {
			var bp = new BlockPacket(block_ts);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.Sign, "Sign");
			EnsureBasics(bp, true);
			Assert.True(bp.SignText == SignText, $"bp.SignText ({bp.SignText}) != SignText ({SignText})");
			Assert.True(bp.SignType == SignType, $"bp.SignType ({bp.SignType}) != SignType ({SignType})");
		}

		[Fact]
		[Category("BlockPacket")]
		public void BlockPacket_WP() {
			var bp = new BlockPacket(block_wp);

			TypeBlockPacket(bp, BlockPacket.BlockIdentifier.WorldPortal, "WorldPortal");
			EnsureBasics(bp, true);
			Assert.True(bp.WorldTarget == WorldTarget, $"bp.WorldTarget ({bp.WorldTarget}) != WorldTarget ({WorldTarget})");
		}
		#endregion

		private void MessagesEqual(Message main, Message comparing) {
			Assert.True(main.Type == comparing.Type, $"main.Type ({main.Type}) != comparing.Type ({comparing.Type})");

			for (uint i = 0; i < main.Count; i++) {
				Assert.True(main[i].GetType().Equals(comparing[i].GetType()), $"m[{i}]'s type ({main[i].GetType()}) != comparing[{i}]'s type ({comparing[i].GetType()})");
				Assert.True(main[i].Equals(comparing[i]), $"main[{i}] ({main[i]}) != comparing[{i}] ({comparing[i]})");
			}
		}

		private void EnsureBasics(BlockPacket bp, bool checkLayer = false) {
			Assert.True(bp.Deserialized == true, $"bp.Deserialized ({bp.Deserialized}) != true ({true})");

			if(checkLayer)
				Assert.True(bp.Layer == Layer, $"bp.Layer ({bp.Layer}) != Layer ({Layer})");

			Assert.True(bp.X == X, $"bp.X ({bp.X}) != X ({X})");
			Assert.True(bp.Y == Y, $"bp.Y ({bp.Y}) != Y ({Y})");
			Assert.True(bp.PlayerId == PID, $"bp.PlayerId ({bp.PlayerId}) != PID ({PID})");

			switch(bp.BlockType) {
				case BlockPacket.BlockIdentifier.Plain: {
					IdBlockPacket(bp, Plain, "Plain");
				}
				break;

				case BlockPacket.BlockIdentifier.NumberValue: {
					IdBlockPacket(bp, NumberValue, "NumberValue");
				}
				break;

				case BlockPacket.BlockIdentifier.NPC: {
					IdBlockPacket(bp, NPCId, "NPCId");
				}
				break;

				case BlockPacket.BlockIdentifier.Morphable: {
					IdBlockPacket(bp, Morphable, "Morphable");
				}
				break;

				case BlockPacket.BlockIdentifier.Sound: {
					IdBlockPacket(bp, Sound, "Sound");
				}
				break;

				case BlockPacket.BlockIdentifier.Label: {
					IdBlockPacket(bp, Label, "Label");
				}
				break;

				case BlockPacket.BlockIdentifier.Portal: {
					IdBlockPacket(bp, Portal, "Portal");
				}
				break;

				case BlockPacket.BlockIdentifier.Sign: {
					IdBlockPacket(bp, SignId, "SignId");
				}
				break;

				case BlockPacket.BlockIdentifier.WorldPortal: {
					IdBlockPacket(bp, WorldPortalId, "WorldPortalId");
				}
				break;
			}
		}

		private void IdBlockPacket(BlockPacket bp, uint TestFor, string nameof) {
			Assert.True(bp.BlockId == TestFor, $"bp.BlockId ({bp.BlockId}) != {nameof} ({TestFor})");
		}

		private void TypeBlockPacket(BlockPacket bp, BlockPacket.BlockIdentifier bId, string strname) {
			Assert.True(bp.BlockType == bId, $"bp.BlockType ({bp.BlockType}) != BlockIdentifier.{strname} ({bId.ToString()})");
		}
	}
}
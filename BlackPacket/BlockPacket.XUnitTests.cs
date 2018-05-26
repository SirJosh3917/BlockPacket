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
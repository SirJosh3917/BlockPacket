using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirJosh3917 {
	/// <summary>
	/// Up to the test for V 226
	/// </summary>
	public class BlockPacket {
		/// <summary>
		/// Deserialize ANY message, ranging from 'block' to 'b', 'br', 'bs', e.t.c
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static BlockPacket Deserialize(PlayerIOClient.Message e) {
			return new BlockPacket(e);
		}

		/// <summary>
		/// Auto deserialize given message
		/// </summary>
		/// <param name="e"></param>
		public BlockPacket(PlayerIOClient.Message e) {
			Deserialized = false;

			#region Deserialize 'block'
			if (IsValidCustomBlockMessage(e)) {
				//We're recieving back one of our special packets!

				uint playerid = e.GetUInt(0);
				uint layer = e.GetUInt(1);
				uint x = e.GetUInt(2);
				uint y = e.GetUInt(3);
				uint blockid = e.GetUInt(4);
				uint blocktype = e.GetUInt(5);
				uint numbervalue = 0, morphvalue = 0, soundvalue = 0, portalrotation = 0, portalid = 0, portaltarget = 0, signtype = 0;
				string labeltext = "", labelcolor = "", signtext = "", worldtarget = "";

				if (!Enum.IsDefined(typeof(BlockIdentifier), (uint)BlockType))
					throw new Exception("Invalid unigned integer used for enum.");

				this.PlayerId = playerid;
				this.Layer = layer;
				this.X = x;
				this.Y = y;
				this.BlockId = blockid;
				this.BlockType = (BlockIdentifier)blocktype;

				switch (blocktype) {
					case 0: { //plain block
						return;
					}
					case 1: {
						numbervalue = e.GetUInt(6);
						break;
					}
					case 2: {
						morphvalue = e.GetUInt(6);
						break;
					}
					case 3: {
						soundvalue = e.GetUInt(6);
						break;
					}
					case 4: {
						labeltext = e.GetString(6);
						labelcolor = e.GetString(7);
						break;
					}
					case 5: { //portal
						portalrotation = e.GetUInt(6);
						portalid = e.GetUInt(7);
						portaltarget = e.GetUInt(8);
						break;
					}
					case 6: { //sign
						signtext = e.GetString(6);
						signtype = e.GetUInt(7);
						break;
					}
					case 7: { //world portal
						worldtarget = e.GetString(6);
						break;
					}
				}

				this.NumberValue = numbervalue;
				this.MorphableValue = morphvalue;
				this.SoundValue = soundvalue;
				this.LabelText = labeltext;
				this.LabelColor = labelcolor;
				this.PortalRotation = portalrotation;
				this.PortalId = portalid;
				this.PortalTarget = portaltarget;
				this.SignText = signtext;
				this.SignType = signtype;
				this.WorldTarget = worldtarget;

				Deserialized = true;

				return;
			}
			#endregion

			#region Deserialize 'b' and the likes
			switch (e.Type) {
				case "b": {
					if (e.Count > 4)
						if (e[0] is int &&
							e[1] is uint &&
							e[2] is uint &&
							e[3] is uint &&
							e[4] is uint) {
							int layer = e.GetInt(0);
							uint x = e.GetUInt(1);
							uint y = e.GetUInt(2);
							uint blockId = e.GetUInt(3);
							uint playerId = e.GetUInt(4);

							if (layer < 0) throw new Exception("Layer Integer is less than 0, not suitable for integer to uint conversion.");

							this.PlayerId = playerId;
							this.Layer = (uint)layer;
							this.X = x;
							this.Y = y;
							this.BlockId = blockId;
							this.BlockType = BlockIdentifier.Plain;

							Deserialized = true;
						}
				} break;

				case "bc": {
					if (e.Count > 4)
						if (e[0] is uint &&
							e[1] is uint &&
							e[2] is uint &&
							e[3] is uint &&
							e[4] is uint) {
							uint layer = 0; //sadly we have to hardcode values :/
							uint x = e.GetUInt(0);
							uint y = e.GetUInt(1);
							uint blockId = e.GetUInt(2);
							uint numbervalue = e.GetUInt(3);
							uint playerId = e.GetUInt(4);

							if (layer < 0) throw new Exception("Layer Integer is less than 0, not suitable for integer to uint conversion.");

							this.PlayerId = playerId;
							this.Layer = layer;
							this.X = x;
							this.Y = y;
							this.BlockId = blockId;
							this.BlockType = BlockIdentifier.NumberValue;

							this.NumberValue = numbervalue;

							Deserialized = true;
						}
				} break;

				case "br": {
					if (e.Count > 5)
						if (e[0] is uint &&
							e[1] is uint &&
							e[2] is uint &&
							e[3] is uint &&
							e[4] is int &&
							e[5] is uint) {
							uint x = e.GetUInt(0);
							uint y = e.GetUInt(1);
							uint blockId = e.GetUInt(2);
							uint morph = e.GetUInt(3);
							int layer = e.GetInt(4);
							uint playerId = e.GetUInt(5);

							if (layer < 0) throw new Exception("Layer Integer is less than 0, not suitable for integer to uint conversion.");

							this.PlayerId = playerId;
							this.Layer = (uint)layer;
							this.X = x;
							this.Y = y;
							this.BlockId = blockId;
							this.BlockType = BlockIdentifier.Morphable;

							this.MorphableValue = morph;

							Deserialized = true;
						}
				} break;

				case "bs": {
					if (e.Count > 4)
						if (e[0] is uint &&
							e[1] is uint &&
							e[2] is uint &&
							e[3] is int &&
							e[4] is uint) {
							int layer = 0; //unfortunate hardcoding
							uint x = e.GetUInt(0);
							uint y = e.GetUInt(1);
							uint blockId = e.GetUInt(2);
							uint sound = e.GetUInt(3);
							uint playerId = e.GetUInt(5);

							if (layer < 0) throw new Exception("Layer Integer is less than 0, not suitable for integer to uint conversion.");

							this.PlayerId = playerId;
							this.Layer = (uint)layer;
							this.X = x;
							this.Y = y;
							this.BlockId = blockId;
							this.BlockType = BlockIdentifier.Sound;

							this.SoundValue = sound;

							Deserialized = true;
						}
				} break;

				case "lb": {
					if (e.Count > 5)
						if (e[0] is uint &&
							e[1] is uint &&
							e[2] is int &&
							e[3] is string &&
							e[4] is string &&
							e[5] is uint) {
							int layer = 0; //unfortunate hardcoding
							uint x = e.GetUInt(0);
							uint y = e.GetUInt(1);
							int blockId = e.GetInt(2);
							string text = e.GetString(3);
							string textColor = e.GetString(4);
							uint playerId = e.GetUInt(5);

							if (layer < 0) throw new Exception("Layer Integer is less than 0, not suitable for integer to uint conversion.");
							if (blockId < 0) throw new Exception("BlockId Integer is less than 0, not suitable for integer to uint conversion.");

							this.PlayerId = playerId;
							this.Layer = (uint)layer;
							this.X = x;
							this.Y = y;
							this.BlockId = (uint)blockId;
							this.BlockType = BlockIdentifier.Label;

							this.LabelText = text;
							this.LabelColor = textColor;

							Deserialized = true;
						}
				} break;

				case "pt": {
					if (e.Count > 6)
						if (e[0] is uint &&
							e[1] is uint &&
							e[2] is uint &&
							e[3] is uint &&
							e[4] is uint &&
							e[5] is uint &&
							e[6] is uint) { /* THE PROTOCOL SAYS THAT THIS IS A INT. */
							int layer = 0; //unfortunate hardcoding
							uint x = e.GetUInt(0);
							uint y = e.GetUInt(1);
							uint blockId = e.GetUInt(2);
							uint portalRotation = e.GetUInt(3);
							uint portalId = e.GetUInt(4);
							uint portalTarget = e.GetUInt(5);
							uint playerId = e.GetUInt(6);

							if (layer < 0) throw new Exception("Layer Integer is less than 0, not suitable for integer to uint conversion.");

							this.PlayerId = playerId;
							this.Layer = (uint)layer;
							this.X = x;
							this.Y = y;
							this.BlockId = blockId;
							this.BlockType = BlockIdentifier.Portal;

							this.PortalRotation = portalRotation;
							this.PortalId = portalId;
							this.PortalTarget = portalTarget;

							Deserialized = true;
						}
				} break;

				case "ts": {
					if (e.Count > 5)
						if (e[0] is uint &&
							e[1] is uint &&
							e[2] is uint &&
							e[3] is string &&
							e[4] is uint &&
							e[5] is uint) {
							int layer = 0; //unfortunate hardcoding
							uint x = e.GetUInt(0);
							uint y = e.GetUInt(1);
							uint blockId = e.GetUInt(2);
							string text = e.GetString(3);
							uint signType = e.GetUInt(4);
							uint playerId = e.GetUInt(5);

							if (layer < 0) throw new Exception("Layer Integer is less than 0, not suitable for integer to uint conversion.");

							this.PlayerId = playerId;
							this.Layer = (uint)layer;
							this.X = x;
							this.Y = y;
							this.BlockId = (uint)blockId;
							this.BlockType = BlockIdentifier.Sign;

							this.SignText = text;
							this.SignType = signType;

							Deserialized = true;
						}
				} break;

				case "wp": {
					if (e.Count > 4)
						if (e[0] is uint &&
							e[1] is uint &&
							e[2] is uint &&
							e[3] is string &&
							e[4] is uint) {
							int layer = 0; //unfortunate hardcoding
							uint x = e.GetUInt(0);
							uint y = e.GetUInt(1);
							uint blockId = e.GetUInt(2);
							string target = e.GetString(3);
							uint playerId = e.GetUInt(4);

							if (layer < 0) throw new Exception("Layer Integer is less than 0, not suitable for integer to uint conversion.");

							this.PlayerId = playerId;
							this.Layer = (uint)layer;
							this.X = x;
							this.Y = y;
							this.BlockId = blockId;
							this.BlockType = BlockIdentifier.WorldPortal;

							this.WorldTarget = target;

							Deserialized = true;
						}
				} break;
			}
			#endregion
		}

		#region serialize
		private int IntForm(uint b) {
			uint manip = b;
			while (manip > (uint)int.MaxValue)
				manip -= (uint)int.MaxValue;
			return (int)manip;
		}

		/// <summary>
		/// Serialize this BlockPacket to a Sendable B, a tl;dr 'b' packet that you can send directly to place the exact same block.
		/// </summary>
		/// <returns></returns>
		public PlayerIOClient.Message SerializeToSendableB() {
			PlayerIOClient.Message build = PlayerIOClient.Message.Create("b");

			build.Add(	IntForm(Layer),
						IntForm(X),
						IntForm(Y),
						IntForm(BlockId));

			switch (BlockType) {
				case BlockIdentifier.Plain: {

				} break;
				case BlockIdentifier.NumberValue: {
					build.Add(IntForm(NumberValue));
				} break;
				case BlockIdentifier.Morphable: {
					build.Add(IntForm(MorphableValue));
				} break;
				case BlockIdentifier.Sound: {
					build.Add(IntForm(SoundValue));
				} break;
				case BlockIdentifier.Label: {
					build.Add(LabelText);
					build.Add(LabelColor);
				} break;
				case BlockIdentifier.Portal: {
					build.Add(IntForm(PortalRotation));
					build.Add(IntForm(PortalId));
					build.Add(IntForm(PortalTarget));
				} break;
				case BlockIdentifier.Sign: {
					build.Add(SignText);
					build.Add(SignType);
				} break;
				case BlockIdentifier.WorldPortal: {
					build.Add(WorldTarget);
				} break;
				default: {
					throw new Exception("Invalid BlockIdentifier.");
				}
			}

			return build;
		}

		/// <summary>
		/// Serialize this BlockPacket to one of EE's 'b' messages, can be interpreted and used for various classes and the likes. Will return 'b', 'bc', 'bs', 'pt', 'ts', e.t.c e.t.c exactly as the packet should be ( int and all ).
		/// </summary>
		/// <returns></returns>
		public PlayerIOClient.Message SerializeToB() {
			switch (BlockType) {
				case BlockIdentifier.Plain: {
					return PlayerIOClient.Message.Create(
						"b",
						IntForm(Layer),
						X,
						Y,
						BlockId,
						PlayerId
						);
				}
				case BlockIdentifier.NumberValue: {
					return PlayerIOClient.Message.Create(
						"bc",
						X,
						Y,
						BlockId,
						NumberValue,
						PlayerId
						);
				}
				case BlockIdentifier.Morphable: {
					return PlayerIOClient.Message.Create(
						"br",
						X,
						Y,
						BlockId,
						MorphableValue,
						IntForm(Layer),
						PlayerId
						);
				}
				case BlockIdentifier.Sound: {
					return PlayerIOClient.Message.Create(
						"bs",
						X,
						Y,
						BlockId,
						IntForm(SoundValue),
						PlayerId
						);
				}
				case BlockIdentifier.Label: {
					return PlayerIOClient.Message.Create(
						"lb",
						X,
						Y,
						IntForm(BlockId),
						LabelText,
						LabelColor,
						PlayerId
						);
				}
				case BlockIdentifier.Portal: {
					return PlayerIOClient.Message.Create(
						"pt",
						X,
						Y,
						BlockId,
						PortalRotation,
						PortalId,
						PortalTarget,
						PlayerId
						);
				}
				case BlockIdentifier.Sign: {
					return PlayerIOClient.Message.Create(
						"ts",
						X,
						Y,
						BlockId,
						SignText,
						SignType,
						PlayerId
						);
				}
				case BlockIdentifier.WorldPortal: {
					return PlayerIOClient.Message.Create(
						"bs",
						X,
						Y,
						BlockId,
						WorldTarget,
						PlayerId
						);
				}
			}
			throw new Exception("Invalid BlockIdentifier.");
		}

		/// <summary>
		/// Serialize this BlockPacket to the custom 'block' packet that I, ninjasupeatsninja, propose EE to use..
		/// </summary>
		/// <returns></returns>
		public PlayerIOClient.Message SerializeToBlockPacket() {
			PlayerIOClient.Message build = PlayerIOClient.Message.Create("block");

			if (!Enum.IsDefined(typeof(BlockIdentifier), (uint)BlockType))
				throw new Exception("Invalid unigned integer used for enum.");

			build.Add(PlayerId);
			build.Add(Layer);
			build.Add(X);
			build.Add(Y);
			build.Add(BlockId);
			build.Add((uint)BlockType);

			switch (BlockType) {
				case BlockIdentifier.Plain: {

				} break;

				case BlockIdentifier.NumberValue: {
					build.Add(this.NumberValue);
				} break;

				case BlockIdentifier.Morphable: {
					build.Add(this.MorphableValue);
				} break;

				case BlockIdentifier.Sound: {
					build.Add(this.SoundValue);
				} break;

				case BlockIdentifier.Label: {
					build.Add(this.LabelText);
					build.Add(this.LabelColor);
				} break;

				case BlockIdentifier.Portal: {
					build.Add(this.PortalRotation);
					build.Add(this.PortalId);
					build.Add(this.PortalTarget);
				} break;

				case BlockIdentifier.Sign: {
					build.Add(this.SignText);
					build.Add(this.SignType);
				} break;

				case BlockIdentifier.WorldPortal: {
					build.Add(this.WorldTarget);
				} break;

				default: {
					throw new Exception("Undefined BlockIdentifier encountered.");
				}
			}

			if (IsValidCustomBlockMessage(build))
				return build;
			else
				throw new Exception("Invalid 'build' message created - this is a weird internal exception! Perhaps you're changing my class' code?");
		}
		#endregion

		#region packet checkers
		/// <summary>
		/// Check if a packet type is a 
		/// 'b'
		/// 'bc'
		/// 'br'
		/// 'bs'
		/// 'lb'
		/// 'pt'
		/// 'ts'
		/// 'wp'
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static bool IsValidEEBlockMessage(PlayerIOClient.Message e) {
			var i = e.Type;
			if (i == "b" ||  //Plain
					i == "bc" || //Number value
					i == "br" || //Morphable
					i == "bs" || //Sound
					i == "lb" || //Label
					i == "pt" || //Portal
					i == "ts" || //Sign
					i == "wp")   //World Portal

				switch (e.Type) {
					case "b": {
						if (e.Count > 4)
							if (e[0] is int &&
								e[1] is uint &&
								e[2] is uint &&
								e[3] is uint &&
								e[4] is uint) {
								return true;
							}
					} break;

					case "bc": {
						if (e.Count > 4)
							if (e[0] is uint &&
								e[1] is uint &&
								e[2] is uint &&
								e[3] is uint &&
								e[4] is uint) {
								return true;
							}
					} break;

					case "br": {
						if (e.Count > 5)
							if (e[0] is uint &&
								e[1] is uint &&
								e[2] is uint &&
								e[3] is uint &&
								e[4] is int &&
								e[5] is uint) {
								return true;
							}
					} break;

					case "bs": {
						if (e.Count > 4)
							if (e[0] is uint &&
								e[1] is uint &&
								e[2] is uint &&
								e[3] is int &&
								e[4] is uint) {
								return true;
							}
					} break;

					case "lb": {
						if (e.Count > 5)
							if (e[0] is uint &&
								e[1] is uint &&
								e[2] is int &&
								e[3] is string &&
								e[4] is string &&
								e[5] is uint) {
								return true;
							}
					} break;

					case "pt": {
						if (e.Count > 6)
							if (e[0] is uint &&
								e[1] is uint &&
								e[2] is uint &&
								e[3] is uint &&
								e[4] is uint &&
								e[5] is uint &&
								e[6] is uint) {
								return true;
							}
					} break;

					case "ts": {
						if (e.Count > 5)
							if (e[0] is uint &&
								e[1] is uint &&
								e[2] is uint &&
								e[3] is string &&
								e[4] is uint &&
								e[5] is uint) {
								return true;
							}
					} break;

					case "wp": {
						if (e.Count > 4)
							if (e[0] is uint &&
								e[1] is uint &&
								e[2] is uint &&
								e[3] is string &&
								e[4] is uint) {
								return true;
							}
					} break;
				}

			return false;
		}

		/// <summary>
		/// Check if a packet matches the requirement for a 'block' packet. 
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static bool IsValidCustomBlockMessage(PlayerIOClient.Message e) {
			if(e.Type == "block")
			if(e.Count > 5)
				if (e[0] is uint && //valid base parameters
					e[1] is uint &&
					e[2] is uint &&
					e[3] is uint &&
					e[4] is uint &&
					e[5] is uint) {

					switch (e.GetUInt(5)) {
						case 0: { //plain block
							return true;
						} break;
						case 1:
						case 2:
						case 3: { //number value / morph / sound
							if (e.Count > 6)
								return e[6] is uint;
						} break;
						case 4: { // label
							if (e.Count > 7)
								return e[6] is string &&
									e[7] is string;
						} break;
						case 5: { //portal

							if (e.Count > 8)
								return e[6] is uint &&
									e[7] is uint &&
									e[8] is uint;
						} break;
						case 6: { //sign

							if (e.Count > 7)
								return e[6] is string &&
									e[7] is uint;
						} break;
						case 7: { //world portal
							if (e.Count > 6)
								return e[6] is string;
						} break;
					}
				}

			return false;
		}
		#endregion

		#region variables
		/// <summary> If deserialization completed with no errors. Useful for debugging. </summary>
		public bool Deserialized { get; private set; }

		public uint PlayerId { get; private set; }
		public uint Layer { get; private set; }
		public uint X { get; private set; }
		public uint Y { get; private set; }
		public uint BlockId { get; private set; }
		public BlockIdentifier BlockType { get; private set; }

		#region extra block params
		//Plain

		//Number
		public uint NumberValue { get; private set; }

		//Morphable
		public uint MorphableValue { get; set; }

		//Sound
		public uint SoundValue { get; set; }

		//Label
		public string LabelText { get; private set; }
		public string LabelColor { get; private set; }

		//Portal
		public uint PortalRotation { get; private set; }
		public uint PortalId { get; private set; }
		public uint PortalTarget { get; private set; }

		//SignBlock
		public string SignText { get; private set; }
		public uint SignType { get; private set; }

		//WorldPortal
		public string WorldTarget { get; private set; }
		#endregion

		public enum BlockIdentifier : uint {
			Plain = 0,
			NumberValue = 1,
			Morphable = 2,
			Sound = 3,
			Label = 4,
			Portal = 5,
			Sign = 6,
			WorldPortal = 7
		}
		#endregion
	}
}

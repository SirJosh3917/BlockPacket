using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SirJosh3917 {
	public static class MessageAlgorithms {
		
		/*
		 * About twice as fast as V1
		 * 
		 * Takes:
		 * 
		 * This:
		 *      267,082,559 ticks
		 *      63,094 ms
		 * 
		 * Other:
		 *      473,521,805 ticks
		 *      111,862 ms
		 * 
		 */
		public static string[] SimpleOverloopV2(string beginning, string custom, int max) {
			int originalMax = max;

			max = max - beginning.Length;

			//ensure the message needs formatting

			if (!(beginning != null && custom != null && max > 0))
				return null;

			if (0 >= max)
				return null;

			if (beginning.Length + custom.Length <= max)
				return new string[] { beginning + custom };

			//separate message into chunks with spaces

			List<string> spaceChunks = new List<string>();
			var chars = custom.ToCharArray();
			int lastSpace = 0;

			for (int i = 0; i < chars.Length; i++)
				if (i - lastSpace > max || char.Equals(chars[i], ' ')) {
					spaceChunks.Add(custom.Substring(lastSpace, i - lastSpace));
					lastSpace = i;
				}

			//trim down the space chunks

			for (int i = 0; i < spaceChunks.Count; i++) {
				var trimmedMsg = spaceChunks[i].Trim();

				if (trimmedMsg.Length < 1)
					spaceChunks.RemoveAt(i--);
				else
					spaceChunks[i] = trimmedMsg;
			}

			//total up the message chunks into messages

			List<string> messageChunks = new List<string>();
			StringBuilder strb = new StringBuilder();

			int lenTotal = 0;

			for (int i = 0; i < spaceChunks.Count; i++) {
				int thisMsgLen = spaceChunks[i].Length;

				if (lenTotal + thisMsgLen > max) {
					messageChunks.Add(strb.ToString());
					strb.Clear();

					strb.Append(spaceChunks[i]);
					lenTotal = thisMsgLen;
				} else {
					strb.Append(" ");
					strb.Append(spaceChunks[i]);
					lenTotal += thisMsgLen;
				}
			}

			//trim every message

			for (int i = 0; i < messageChunks.Count; i++) {
				var trimmedMsg = messageChunks[i].Trim();

				if (trimmedMsg.Length < 1)
					messageChunks.RemoveAt(i--);
				else
					messageChunks[i] = trimmedMsg;
			}

			//append every message to the beginning custom message

			List<string> messages = new List<string>();

			for (int i = 0; i < messageChunks.Count; i++)
				messages.Add(beginning + messageChunks[i]);

			//trim messages just incase

			for (int i = 0; i < messages.Count; i++) {
				var trimmedMsg = messages[i].Trim();

				if (trimmedMsg.Length < 1)
					messages.RemoveAt(i--);
				else
					messages[i] = trimmedMsg;
			}

			//release

			return messages.ToArray();
		}

		/* 
		 * Used for PMs:
		 * '/pm johnathon <oh hello johnathon! this message is way over 140 charecters, but this algorithm will automatically crop this message to the'
		 * '/pm johnathon proportional length with advanced space-before-message-ending for seamless message overloops>'
		 */
		public static string[] SimpleOverloopV1(string beginning, string custom, int max) {
			
			if (!(beginning != null && custom != null && max > 0))
				return null;
			
			//ensure the message needs formatting

			if (beginning.Length + custom.Length <= max)
				return new string[] { beginning + custom };

			if (0 >= max)
				return null;

			//message is too long, let's start formatting it into bits.

			int maxPerLine = max - beginning.Length;
			int maxLineCounter = maxPerLine;

			if (maxPerLine < 0) //handle error properly [ TODO FIX ]
				return null;

			var msgs = new List<string>();

			for (int i = 0; i < custom.Length; i++) {
				//once the counter reaches an unacceptable length
				if (i >= maxLineCounter) {
					msgs.Add(custom.Substring(i - maxPerLine, maxPerLine)); //substring the message
					maxLineCounter += maxPerLine; //add to counter
				}

				if (i >= custom.Length - 1) {
					msgs.Add(custom.Substring(maxLineCounter - maxPerLine, custom.Length - (maxLineCounter - maxPerLine)));
				}
			}

			//message formatted into bits, let's now find spaces and space out the message ( repeat untill message is cut to size )

			for (int i = 0; i < msgs.Count; i++) {
				var msgMaxLen = (msgs[i].Length > maxPerLine ? msgs[i].Substring(0, maxPerLine) : msgs[i]);

				//message has space
				if (msgMaxLen.Contains(' ') && msgs[i].Length >= maxPerLine) {

					//take the last space and move it over into the next word
					var msgsSpaceSplit = msgs[i].Substring(0, maxPerLine).Split(' ');
					var locationOfLastSpace = msgsSpaceSplit.Length - 1;

					//ensure there's another line always ( only if needed )
					if (msgs.Count <= i + 1)
						msgs.Add("");

					//take the space before the word and shove it into the next sentence
					msgs[i + 1] = msgsSpaceSplit[locationOfLastSpace] + msgs[i].Substring(maxPerLine) + msgs[i + 1];

					//remove space from own sentence
					msgsSpaceSplit[locationOfLastSpace] = "";

					StringBuilder strb = new StringBuilder();

					for (int j = 0; j < msgsSpaceSplit.Length; j++) {
						strb.Append(msgsSpaceSplit[j] + " ");
					}

					//reformat message
					msgs[i] = strb.ToString().Trim();
				} else if (!msgMaxLen.Contains(' ')) {
					if (msgMaxLen.Length >= maxPerLine) {
						msgs.Add(msgMaxLen.Substring(maxPerLine) + msgs[i + 1]);
						msgs.RemoveAt(i + 1);
					}
				}
			}

			//make sure that all the messages meet the required max length
			bool keepShifting = true;
			while (keepShifting) {
				keepShifting = false;
				for (int i = 0; i < msgs.Count; i++) {
					if (msgs[i].Length > maxPerLine) { //only if the message is too long
						keepShifting = true;
						//shift over words that have space ( copy pasted code from above )

						//take the last space and move it over into the next word
						var msgsSpaceSplit = msgs[i].Substring(0, maxPerLine).Split(' ');
						var locationOfLastSpace = msgsSpaceSplit.Length - 1;

						//ensure there's another line always ( only if needed )
						if (msgs.Count <= i + 1)
							msgs.Add("");

						//take the space before the word and shove it into the next sentence
						msgs[i + 1] = msgsSpaceSplit[locationOfLastSpace] + msgs[i].Substring(maxPerLine) + msgs[i + 1];

						//remove space from own sentence
						msgsSpaceSplit[locationOfLastSpace] = "";

						StringBuilder strb = new StringBuilder();

						for (int j = 0; j < msgsSpaceSplit.Length; j++) {
							strb.Append(msgsSpaceSplit[j] + " ");
						}

						//reformat message
						msgs[i] = strb.ToString().Trim();
					}
				}
			}

			//return array of messages to send
			for (int i = 0; i < msgs.Count; i++)
				msgs[i] = beginning + msgs[i];

			return msgs.ToArray();
		}
	}
}

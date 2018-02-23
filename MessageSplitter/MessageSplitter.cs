using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SirJosh3917 {
	public static class MessageAlgorithms {

		/* 
		 * Used for PMs:
		 * '/pm johnathon <oh hello johnathon! this message is way over 140 charecters, but this algorithm will automatically crop this message to the'
		 * '/pm johnathon proportional length with advanced space-before-message-ending for seamless message overloops>'
		 */
		public static string[] SimpleOverloop(string beginning, string custom, int max) {
			
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

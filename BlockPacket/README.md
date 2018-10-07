# BlockPacket
My proposition to EE's inconsistency with 'b', 'br', 'bs', and the likes by making a class to handle it all, anything you want,

Note: *Packet* should be refered as a *PlayerIOClient.Message*.

## Why would I use this?
BlockPacket is a step towards a more consistent EE. The main problem BlockPacket addresses is the fact that EE uses many different block type packets for every block. 'b' for basic blocks, 'bc' for number value blocks, 'br' for morphs, 'pt' for portals, and the list goes on and on.

There are a total of 9 different packets EE will send for a block. 9! That's a lot of packets to handle.

BlockPacket does the heavy lifting and grunting of handling every kind of block packet, and bundles it into a neat package, with additional pluses two.

For one, you can just quickly add it to your OnMessage to get setup with it, and not have to worry about handling the 9 different kinds of block packets.

Secondly, BlockPacket handles blocks with each block given their own special property, right in the class. Reference `PlayerId` for the player's Id, instead of having to handle and parse that, and the same can be said with any property.

Thirdly, BlockPacket is capable of handling it's custom `block` packet, as well as any of EE's 9 different packets you throw at it, but did you know that it can also *serialize* them too? You can seriailize a BlockPacket to 3 different kind of packets:

 - One of EE's 9 different packets
 - The custom `block` packet
 - A sendable block packet
 
 With the sendable block packet, you can send this packet off to EE and EE will handle it properly, as it should. No fuss about it!
 
 **tl;dr**: With BlockPacket, you will be able to:
 
  1. Handle all of EE's 9 different packets
  2. Reference each property of a block by a property in the class
  3. Serialize to the custom `block` packet
  4. Serialize to one of EE's 9 different packtes
  5. Seriailze to a sendable `b` packet, ready for sending.
  
## Sounds great, where do I start?

Start by importing the class into your project, just create a new class, copy and paste all of the code into it, and add `using SirJosh3917;` to the top of your projects wherever you desire to use BlockPacket.

If you'd like to implement the OnMessage features, use this code at the top of your OnMessage:

		static void OnMessage(object sender, PlayerIOClient.Message m) {
			BlockPacket bpr = null;
			if (BlockPacket.IsValidEEBlockMessage(e) ||
				BlockPacket.IsValidCustomBlockMessage(e)) {
				bpr = new BlockPacket(m);
			}
      
And then use bpr accordingly!

## What features does the BlockPacket class have?

`new BlockPacket(e);`

When you create a `new BlockPacket(e);`, assume the 'e' is a PlayerIOClient.Message, and that the 'e' message is the message you waant to deserialize into a BlockPacket. Whether it be a custom `block`, or a `b` packet from EE, BlockPacket has you covered. What it *doesn't* support however, is the turning of a sendable `b` message into a BlockPacket.
___
`bpr.SerializeToSendableB();`

This will serialilze the BlockPacket into a sendable 'b', the form of the class that you can safely send off to EE and EE will treat it like you're sending a normal 'b' packet.
___
`bpr.SerializeToB();`

This will serialize the BlockPacket into one of EE's 8 different packets, so that you can take the packet and use it for whatever purposes you require.
___
`bpr.SerializeToBlockPacket();`

This will serialize the BlockPacket into the *highly suggested* `block` packet. This packet is the **main goal** of BlockPacket to achieve, is the EE Development Team to implement a single and ultimate `block` packet into their servers so that we, the users, won't have to deal with the hassle of handling 9 different packets, and having different variable types all around. It'd make life much easier, and is the agenda for BlockPacket.
___
`BlockPacket.IsValidEEBlockMessage(PlayerIOClient.Message e);`

This will return true if the packet recieved is one of EE's 9 different block packets, and will also check the parameters of the message if they are the correct values. This will ensure that the specific packet in question is safe for use.
___
`BlockPacket.IsValidCustomBlockMessage(PlayerIOClient.Message e);`

This will return true if the packet recieved is the custom `block` packet, and if all the parameters are set correctly. This will ensure that the specific packet in question is safe for use.
___
`bpr.Deserialized;`

If the block packet was deserialized correctly. Returns false if the packet has not been deseriailzied correctly.
___
`bpr.PlayerId;`

PlayerId of the player who placed the block
___
`bpr.NumberValue;`

The NumberValue of the number block
___
`bpr.MorphableValue;`

The MorphableValue of the morphable block
___
`bpr.SoundValue;`

The SoundValue of the sound block
___
`bpr.LabelText;`

The text of a label block
___
`bpr.LabelColor;`

The color of a label block
___
`bpr.PortalRotation;`

The rotation of a portal block
___
`bpr.PortalId;`

The id of a portal block
___
`bpr.PortalTarget;`

The target of a portal block
___
`bpr.SignText;`

The text of a sign block
___
`bpr.SignType;`

The type of a sign block
___
`bpr.WorldTarget;`

The target of a world portal block
___
`bpr.NPCName;`

The name of the NPC
___
`bpr.NPCChat1;`

The first line of chat for the NPC.
___
`bpr.NPCChat2;`

The second line of chat for the NPC.
___
`bpr.NPCChat3;`

The third line of chat for the NPC.
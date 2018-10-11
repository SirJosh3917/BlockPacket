# RevampedBlockPacket

This version of BlockPacket is revamped to be cleaner.

It requires `SirJosh3917.Decorator` as a dependancy.

# Found bugs?

Please report them.

# Usage

## Serialization

```cs
var block = new SimpleBlock {
    Layer = 0,
    BlockId = 9,
    X = 10,
    Y = 20
};

var msg = BlockPacket.Serialize(block);

con.Send(msg); // place the block
```

## Deserialization

```cs
using SirJosh3917.RevampedBlockPacket;
using PlayerIOClient;

static void OnMessage(object sender, Message msg)
{
    if (BlockPacket.TryDeserialize(msg, out var block))
    {
        // block is SirJosh3917.RevampedBlockPacket.BlockTypes.Block
        // you can store `block` in an array or something
        
        if (block is SirJosh3917.RevampedBlockPacket.BlockTypes.MorphableBlock mb)
        {
            // mb is SirJosh3917.RevampedBlockPacket.BlockTypes.MorphableBlock
            // mb.Morph now has the morph
        }
        
        // see SirJosh3917.RevampedBlockPacket.BlockTypes for every block type
    }
}```
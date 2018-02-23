# Message Splitter

This is a simple class to split a long message into separate messages.

## Usage

If you want to split a PM into multiple messages with the right charecter length, call the following:

`var sendMsgs = SimpleOverloop("/pm ninjasupeatsninja ", "... this message is more than 120 chars ...", 120);`

This will give you a string[] of each message you need to send back to the use.

Iterate over it and send it off.

`for(int i = 0; i < sendMsgs.Length; i++) { con.Send("say", sendMsgs[i]); }`

It also automatically handles spaces in messages for nice formatting.

## Example


```
var sendMsgs = SirJosh3917.MessageAlgorithms.SimpleOverloop("/pm ninjasupeatsninja ", " Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras laoreet turpis quis orci feugiat, eu efficitur nisl finibus. Etiam risus metus, suscipit et hendrerit ac, ultricies eget justo. Nunc nec dapibus ligula, ac convallis erat. Duis lectus est, interdum sit amet ante at, pharetra finibus augue. Sed nisl diam, varius at blandit ut, blandit non nunc. Quisque accumsan dictum tincidunt. Donec mollis scelerisque lectus, nec hendrerit orci cursus in. Mauris ac suscipit mi. In ac eros pellentesque, blandit dui non, tristique nulla. Nunc gravida pharetra lacinia. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Praesent viverra vehicula tellus facilisis vulputate. Suspendisse eu ullamcorper ligula. Quisque sit amet finibus massa. Fusce ante erat, porta at faucibus eget, aliquet id mi. Sed congue tincidunt tellus, quis placerat dolor tempor nec. Pellentesque facilisis leo et elit commodo, sit amet viverra tellus rutrum. Sed elementum tortor arcu, eu posuere metus tempor eu. Sed at tortor quis arcu hendrerit condimentum. Phasellus congue, mauris non imperdiet dictum, justo ante sodales purus, eget posuere urna leo quis erat. Aliquam vel ornare ligula, sollicitudin aliquam mauris. Donec tristique odio a odio vehicula, sit amet dapibus tortor viverra. Phasellus interdum, ex sed tempor vulputate, metus dui aliquet ante, sit amet rutrum enim nulla et dolor. Cras id ullamcorper dolor, eget interdum nulla. Pellentesque vestibulum, arcu eget facilisis tempus, dui turpis congue est, eu accumsan dui neque quis odio. ", 120);
```

```
/pm ninjasupeatsninja Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras laoreet turpis quis orci feugiat,
/pm ninjasupeatsninja eu efficitur nisl finibus. Etiam risus metus, suscipit et hendrerit ac, ultricies eget justo.
/pm ninjasupeatsninja Nunc nec dapibus ligula, ac convallis erat. Duis lectus est, interdum sit amet ante at, pharetra
/pm ninjasupeatsninja finibus augue. Sed nisl diam, varius at blandit ut, blandit non nunc. Quisque accumsan dictum
/pm ninjasupeatsninja tincidunt. Donec mollis scelerisque lectus, nec hendrerit orci cursus in. Mauris ac suscipit mi.
/pm ninjasupeatsninja In ac eros pellentesque, blandit dui non, tristique nulla. Nunc gravida pharetra lacinia. Orci
/pm ninjasupeatsninja varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Praesent
/pm ninjasupeatsninja viverra vehicula tellus facilisis vulputate. Suspendisse eu ullamcorper ligula. Quisque sit amet
/pm ninjasupeatsninja finibus massa. Fusce ante erat, porta at faucibus eget, aliquet id mi. Sed congue tincidunt
/pm ninjasupeatsninja tellus, quis placerat dolor tempor nec. Pellentesque facilisis leo et elit commodo, sit amet
/pm ninjasupeatsninja viverra tellus rutrum. Sed elementum tortor arcu, eu posuere metus tempor eu. Sed at tortor quis
/pm ninjasupeatsninja arcu hendrerit condimentum. Phasellus congue, mauris non imperdiet dictum, justo ante sodales
/pm ninjasupeatsninja purus, eget posuere urna leo quis erat. Aliquam vel ornare ligula, sollicitudin aliquam mauris.
/pm ninjasupeatsninja Donec tristique odio a odio vehicula, sit amet dapibus tortor viverra. Phasellus interdum, ex sed
/pm ninjasupeatsninja tempor vulputate, metus dui aliquet ante, sit amet rutrum enim nulla et dolor. Cras id
/pm ninjasupeatsninja ullamcorper dolor, eget interdum nulla. Pellentesque vestibulum, arcu eget facilisis tempus, dui
/pm ninjasupeatsninja turpis congue est, eu accumsan dui neque quis odio. 
```
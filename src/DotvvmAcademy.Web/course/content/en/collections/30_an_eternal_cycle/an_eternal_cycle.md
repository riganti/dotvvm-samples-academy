# An Eternal Cycle

Note: I find its best to read the parts in italics in a hushed ironical voice.

Per every _valid_ request a ViewModel is born. It begins its life as a humble, empty class and it stays that way
until The Framework invokes its `Init()` method. There, the ViewModel is allowed to use its facades to
gather information and prepare for its future life in general.

Later, our young ViewModel is populated with the data from the client, _provided of course there is some to begin with
(the request is a [postback][postback])_. The Framework, in its benevolence, grants the ViewModel an opportunity to
deal with this new information in its `Load()` method. Perhaps by using more facades?
The Framework doesn't care really.

Then comes the greatest challenge of ViewModel's life, it must execute the client's commandments,
_also called Commands_. At last, when the `PreRender()` method is called, ViewModels's life is nearly over. It is
allowed to make some final changes to its life's work and then gets serialized and sent to the client.

_...only to be reborn again with the next request._

---

TL;DR:

- In `Init()` load the __initial__ data.
- In `Load()` work with the data __loaded__ from the client.
- In `PreRender()` get everything in order before the View is __rendered__.

[postback]: https://stackoverflow.com/questions/183254/what-is-a-postback


I first attempted this kata at an all-day XP Manchester event.
Then I returned to it just out of interest, and for fun.

One interesting thing that came out of it was that the self-shunt method worked fine until the last minute, 
at which point the only way it could remain valid was if I contorted the code for the benefit of the test, 
which I could not justify.

Actually, many interesting things came out of it:

1) The limitations of self-shunt:

As mentioned above, I started out using self shunt, but it fell at the last hurdle because the Grid class created copies of each cell (to avoid making changes to neighbouring cells before their neighbours had a chance to evaluate what their next state should be) – which meant that the test cell was no longer being operated on.
There were ways round this – for instance creating a temp copy of the whole grid and then finding the corresponding entries in the original – but this was less neat and would have meant I was contorting the code for the sake of the test.

2) The danger of checking actions only, and never state:

Because I was testing actions rather than state (which seems to be part of the point of self-shunt?), I only ever tested that Live() and Die() were called, rather than testing the results of those calls. 
The result of this was that although all my tests were passing, when I actually ran the code in a console app, I was mystified to see that all my cells were permanently alive (I started with a grid where all cells were alive).
The reason for this turned out to be that I had never actually implemented Live() and Die() in my GameOfLifeGrid class!

3) Don't forget to run the code!

It wasn’t until I actually ran the code in a simple console app that I spotted one particular problem (for which the solution was to copy all the cells, hence invalidating self-shunt): You can’t loop through the cells, altering the state of each one as you go – this will pollute the state of the surrounding cells, resulting in incorrect results. Luckily I am quite anal, and manually checked the state of every single cell when I ran the code. :)
Obviously I should have thought of this problem in advance, but I didn't. Sometimes you just need to rin the damn code. Which is bloody obvious, but when "playing" with TDD in the context of a kata, you can sometimes forget this.

4) Can Self-Shunt cause problems when the test has to implement methods it shouldn't care about?

When I was still using self-shunt, I found myself potentially having to re-implement certain key methods on ICell within my test class. For instance, I wanted to add an ICell method which would allow you to discover whether two cells were neighbours or not. 
This method would then have to be implemented within my test (which implemented the ICell interface): My test cell would have to inform the grid whether other cells were its neighbours or not. 
But that would mean that my test contained logic which in itself would potentially need testing, and didn’t really belong in the test. 
The solution in the end was to place the method somewhere else: I put it in the GameOfLifeGrid class, but it could have been an external static method (but not an extension method, which would have been nice – because you can’t put an extension method on an interface). 
But although that was probably a more correct approach than to put the method on the ICell interface, the *reason* I did it was to avoid having to put logic in my test class. This worries me. 
I suspect I am misunderstanding how self-shunt should be used, but I know I have also spotted this problem whilst writing self-shunt tests in a production environment (note to self: Do some research to remind self why self-shunt is a good thing - maybe it depends on context?).

5) Tests Which Contain Logic

Speaking of logic in the test class... I hate repetition. I am a big proponent of DRY (Don’t Repeat Yourself). Therefore, in order to avoid repeating the same test-setup logic over and over, I ended up writing a lot of logic into the tests themselves (see GameOfLifeTestHelpers.cs). 
This resulted in some lovely code, but I know there’s an argument to avoid having too much logic in the tests – otherwise you want tests that test the tests! 
However, the alternative would be to either have less test coverage or millions of almost-exactly-the-same tests, which I think would lead to even more problems due to the impossiblity of either reading or maintaining them. Also, you can check your test logic is correct by deliberately forcing the tests to fail (in meaningful ways).

6) Adding Stuff To Already-Written Tests

I found that I was writing a simple test and then going back to extend it, when I noticed other similar scenarios which needed testing. 
For instance: I started out with a test called GivenALiveCellWithOneLiveNeighbour_WhenGameEvolves_ThenCellDies. 
Then this became GivenALiveCellWithOneLiveNeighbourAndTheRestAreDeadNeighbours_WhenGameEvolves_ThenCellDies, when I decided that the rest of the neighbours were also important. 
Then I realised I wanted to check this test would pass whether my cell was on the edge of the grid, on the corner, or in the middle (at this point I didn’t change the name of the test, but I did introduce a helper method called TestAllTypesOfCellPosition). 
Then I decided that the non-neighbours were also important, and the test became GivenALiveCellWithOneLiveNeighbourAndTheRestAreDeadNeighboursAndSomeLiveNonNeighbours_WhenGameEvolves_ThenCellDies. 

Obviously what I could have done was write a new test each time, but the same conditions would apply to all tests – and I wanted to check those extra conditions for all tests – so I could have ended up with an enormous number of tests. Instead, I stuck with the one test – which was testing the one thing: when a live cell has one live neighbour, it should die. But I am also specifying all the other things which should also be true when a live cell has one live neighbour – and if these things are not included in the test, then you are testing a scenario which will never actually happen (in isolation) in a grid of any decent size. 

// Helpful tips - typescript

1. mandatory = () => throw new Error(); 
greet(message = mandatory()) {
  return message;
}

2. ??
message = test ?? 'default value';
messageFalse() ?? messageTrue();
message ??= 'default value';

3. Math.pow = 5**2
4. Ternarary operator
message = arr > 0
   ? value1
   : value2
  ? value3
  : value4
5. Assigning multiple values
const [one, two] = [1, 2];
const [personOneSwap, personTwoSwap] = [personTwo, personOne];
6. Merging arrays
const newArrary = [...one, ...two];

7. Short circuit evaluation with &&
[1, 5, 7].includes(x) && functionOne();

8. Adding object values into an array
const arrTwo = Object.values(credits);

20. find an element within an array
array.find((value) => value.property === 'value');

21. floor value of a number with ~~
Math.floor(5.25)
~~5.25

22. Checking the presence of an item in array
let numbers = [1, 2, 3, 4];l
if (numbes.include(1)) {
 console.log('one is present');
}

23. Use Array.reduce() when you want to convert an array down to a single value by manipulating its values.
24. Use Array.find() when you want to get the first item of an array that passes an explicitly defined test.
25. Array.every() = when you wnat to confirm that every item of an array passes an explicitly defined condition
26. Array.some()

/// Helpful rxjs code

1. Avoid logic inside the subscribe function

```<code>
// wrong
pokemon$.subscribe((pokemon: Pokemon) => {
  if (pokemon.type !== "Water") {
    return;
  }
  const pokemonStats = getStats(pokemon);
  logStats(pokemonStats);
  saveToPokedex(pokemonStats);
});
```

```<code>
// better
pokemon$
  .pipe(
    filter(({ type }) => type === "Water"),
    map(pokemon => getStats(pokemon)),
    tap(stats => logStats(stats))
  )
  .subscribe(stats => saveToPokedex(stats));
```

2. Avoid duplicated logic

```<code>
// wrong
import { from } from "rxjs";
import { filter, reduce } from "rxjs/operators";

const number$ = from([null, 2, 1, undefined, 5, false, 6, 7]);

// Adding numbers
number$
  .pipe(
    filter<number>(Boolean),
    reduce((acc, curr) => acc + curr)
  )
  .subscribe(n => console.log(`Total: ${n}`));

// Emit even numbers
number$
  .pipe(
    filter<number>(Boolean),
    filter(n => n % 2 === 0)
  )
  .subscribe(console.log);

//better
import { from } from "rxjs";
import { filter, reduce } from "rxjs/operators";

const number$ = from([null, 2, 1, 0, 5, false, 6, 7]).pipe(
  filter<number>(Boolean)
);

// Adding numbers
number$
  .pipe(
    reduce((acc, curr) => acc + curr)
  )
  .subscribe(n => console.log(`Total: ${n}`));

// Emit even numbers
number$
  .pipe(
    filter(n => n % 2 === 0)
  )
  .subscribe(console.log);
```

3. Share to avoid stream duplication

```<code>
// wrong
pokemon$ = http.get(/* make an http request here*/);
/*Every time we subscribe to pokemon$, an http request will be made*/

pokemon$
  .pipe(
    flatMap(pokemon => pokemon),
    filter(({ type }) => type === "Fire")
  )
  .subscribe(pokemon => {
    // Do something with pokemon
  });

pokemon$.pipe(switchMap(pokemon => getStats(pokemon))).subscribe(stats => {
  // Do something with stats
});

//better - share() or shareReplay()
pokemon$ = http.get(/* make an http request here*/).pipe(share());
/*The pokemon$ Observable is now hot, we won't have multiple http requests*/

pokemon$
  .pipe(
    flatMap(pokemon => pokemon),
    filter(({ type }) => type === "Fire")
  )
  .subscribe(pokemon => {
    // Do something with pokemon
  });

pokemon$.pipe(switchMap(pokemon => getStats(pokemon))).subscribe(stats => {
  // Do something with stats
});
```

# List<T> Collections - Assessment Questions

## Test_Agent Progressive Challenge System

Complete all 9 levels sequentially to demonstrate mastery of List<T> concepts.

---

## Level 1: Foundation (Trivial)

**Challenge:** Create a list and add elements to it.

**Requirements:**
1. Create an empty `List<string>` for fruit names
2. Add 5 different fruits using `Add()`
3. Display the count and all elements

**Expected Output:**
```
Adding fruits to list...
List count: 5
Fruits: Apple, Banana, Cherry, Date, Elderberry
```

---

## Level 2: Foundation (Trivial)

**Challenge:** Access and modify list elements.

**Requirements:**
1. Create a list of integers: 10, 20, 30, 40, 50
2. Display the first and last elements
3. Modify the middle element to 300
4. Display the updated list

**Expected Output:**
```
Original list: [10, 20, 30, 40, 50]
First element: 10
Last element: 50
Modified list: [10, 20, 300, 40, 50]
```

---

## Level 3: Application (Easy)

**Challenge:** Practice adding and removing elements.

**Requirements:**
1. Create a to-do list with initial items: "Buy groceries", "Call mom", "Finish report"
2. Add "Go to gym" at the end
3. Insert "URGENT: Pay bills" at the beginning
4. Remove "Call mom"
5. Display the list after each operation

**Expected Output:**
```
Initial list:
  1. Buy groceries
  2. Call mom
  3. Finish report

After adding "Go to gym":
  1. Buy groceries
  2. Call mom
  3. Finish report
  4. Go to gym

After inserting "URGENT: Pay bills" at position 0:
  1. URGENT: Pay bills
  2. Buy groceries
  3. Call mom
  4. Finish report
  5. Go to gym

After removing "Call mom":
  1. URGENT: Pay bills
  2. Buy groceries
  3. Finish report
  4. Go to gym
```

---

## Level 4: Application (Easy)

**Challenge:** Search and check list contents.

**Requirements:**
1. Create a list of numbers: 5, 12, 8, 23, 15, 8, 42, 8
2. Check if list contains 23
3. Find the index of first occurrence of 8
4. Find the index of last occurrence of 8
5. Count how many times 8 appears

**Expected Output:**
```
List: [5, 12, 8, 23, 15, 8, 42, 8]

Contains 23? True
First index of 8: 2
Last index of 8: 7
Count of 8: 3
```

---

## Level 5: Integration (Moderate)

**Challenge:** Sort and filter a list of scores.

**Requirements:**
1. Create a list of test scores: 85, 92, 78, 95, 88, 72, 90, 65
2. Display original list
3. Sort ascending and display
4. Find scores above 80 using FindAll
5. Find highest and lowest scores
6. Calculate and display the average

**Expected Output:**
```
Original scores: [85, 92, 78, 95, 88, 72, 90, 65]
Sorted scores: [65, 72, 78, 85, 88, 90, 92, 95]

Scores above 80: [85, 92, 95, 88, 90]
Highest score: 95
Lowest score: 65
Average: 83.13

Passed (≥70): 7 students
Failed (<70): 1 student
```

---

## Level 6: Integration (Moderate)

**Challenge:** Work with a list of custom objects.

**Requirements:**
1. Create a `Product` class with: Name, Price, Quantity
2. Create a list of 5 products
3. Find the most expensive product
4. Find all products with quantity < 10
5. Calculate total inventory value (price × quantity for all)
6. Sort by price and display

**Expected Output:**
```
╔════════════════════════════════════════════════════════════╗
║                    PRODUCT INVENTORY                        ║
╠════════════════════════════════════════════════════════════╣
║ Product          │ Price    │ Qty  │ Value                 ║
╠══════════════════╪══════════╪══════╪═══════════════════════╣
║ Laptop           │ $999.99  │ 5    │ $4,999.95             ║
║ Mouse            │ $29.99   │ 25   │ $749.75               ║
║ Keyboard         │ $79.99   │ 8    │ $639.92               ║
║ Monitor          │ $349.99  │ 12   │ $4,199.88             ║
║ Headphones       │ $149.99  │ 3    │ $449.97               ║
╠════════════════════════════════════════════════════════════╣
║ Total Inventory Value: $11,039.47                          ║
╚════════════════════════════════════════════════════════════╝

Most expensive: Laptop ($999.99)
Low stock (< 10): Laptop (5), Keyboard (8), Headphones (3)

Sorted by price (ascending):
  1. Mouse: $29.99
  2. Keyboard: $79.99
  3. Headphones: $149.99
  4. Monitor: $349.99
  5. Laptop: $999.99
```

---

## Level 7: Mastery (Challenging)

**Challenge:** Implement a contact manager with full CRUD operations.

**Requirements:**
1. Create a `Contact` class with: Name, Phone, Email, Category
2. Implement methods:
   - AddContact
   - RemoveContact (by name)
   - FindContact (by name, partial match)
   - ListByCategory
   - UpdateContact
3. Demonstrate all operations
4. Handle edge cases (not found, duplicates)

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                      CONTACT MANAGER                              ║
╠══════════════════════════════════════════════════════════════════╣

Adding contacts...
✓ Added: Alice Johnson (Family)
✓ Added: Bob Smith (Work)
✓ Added: Carol White (Friend)
✓ Added: David Brown (Work)
✓ Added: Alice Cooper (Friend)

All Contacts:
─────────────────────────────────────────────────────────────────────
Name              │ Phone          │ Email                  │ Category
──────────────────┼────────────────┼────────────────────────┼─────────
Alice Johnson     │ 555-1234       │ alice.j@email.com      │ Family
Bob Smith         │ 555-5678       │ bob.smith@work.com     │ Work
Carol White       │ 555-9012       │ carol@email.com        │ Friend
David Brown       │ 555-3456       │ david.b@work.com       │ Work
Alice Cooper      │ 555-7890       │ alice.c@email.com      │ Friend

Finding "Alice":
  1. Alice Johnson (Family)
  2. Alice Cooper (Friend)

Contacts in "Work" category:
  1. Bob Smith
  2. David Brown

Updating Bob Smith's phone...
✓ Updated: Bob Smith - Phone: 555-0000

Removing Carol White...
✓ Removed: Carol White

Attempting to remove "Unknown Person"...
✗ Contact not found: Unknown Person

Final contact count: 4
```

---

## Level 8: Mastery (Challenging)

**Challenge:** Build a playlist manager with queue operations.

**Requirements:**
1. Create a `Song` class with: Title, Artist, Duration (TimeSpan)
2. Implement playlist operations:
   - AddToEnd, AddToBeginning, InsertAt
   - PlayNext (remove and return first)
   - Shuffle (randomize order)
   - GetTotalDuration
   - FindByArtist
3. Display with formatted durations
4. Show queue state after operations

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════╗
║                       PLAYLIST MANAGER                            ║
╠══════════════════════════════════════════════════════════════════╣

Building playlist...
Added: "Bohemian Rhapsody" - Queen (5:55)
Added: "Stairway to Heaven" - Led Zeppelin (8:02)
Added: "Hotel California" - Eagles (6:31)
Added: "Sweet Child O' Mine" - Guns N' Roses (5:56)
Added: "Comfortably Numb" - Pink Floyd (6:24)

Current Playlist (5 songs, 32:48 total):
─────────────────────────────────────────────────────────────────────
 #  │ Title                    │ Artist           │ Duration
────┼──────────────────────────┼──────────────────┼──────────
 1  │ Bohemian Rhapsody        │ Queen            │ 5:55
 2  │ Stairway to Heaven       │ Led Zeppelin     │ 8:02
 3  │ Hotel California         │ Eagles           │ 6:31
 4  │ Sweet Child O' Mine      │ Guns N' Roses    │ 5:56
 5  │ Comfortably Numb         │ Pink Floyd       │ 6:24

Playing next song...
♪ Now Playing: "Bohemian Rhapsody" by Queen (5:55)

Adding priority song at beginning...
Added to beginning: "Another Brick in the Wall" - Pink Floyd

Current queue (5 songs remaining):
 1. Another Brick in the Wall - Pink Floyd
 2. Stairway to Heaven - Led Zeppelin
 3. Hotel California - Eagles
 4. Sweet Child O' Mine - Guns N' Roses
 5. Comfortably Numb - Pink Floyd

Songs by Pink Floyd:
  - Another Brick in the Wall (4:01)
  - Comfortably Numb (6:24)

Shuffling playlist...
Shuffled order:
 1. Sweet Child O' Mine
 2. Comfortably Numb
 3. Another Brick in the Wall
 4. Stairway to Heaven
 5. Hotel California
```

---

## Level 9: Expert (Very Challenging)

**Challenge:** Create a leaderboard system with ranking and statistics.

**Requirements:**
1. Create a `Player` class with: Name, Score, GamesPlayed, Wins
2. Implement leaderboard:
   - AddPlayer, RecordGame (updates stats)
   - GetTopN (top n players by score)
   - GetRankings (position for each player)
   - GetStatistics (average score, top scorer, most games)
   - FindPlayersByScoreRange
3. Handle ties in ranking
4. Track historical score changes

**Expected Output:**
```
╔══════════════════════════════════════════════════════════════════════╗
║                        GAME LEADERBOARD                               ║
╠══════════════════════════════════════════════════════════════════════╣

Registering players and recording games...

Recording games:
  Alice: Won (+100 points) - Total: 100
  Bob: Lost (+25 points) - Total: 25
  Alice: Won (+100 points) - Total: 200
  Carol: Won (+100 points) - Total: 100
  Bob: Won (+100 points) - Total: 125
  David: Won (+100 points) - Total: 100
  Alice: Lost (+25 points) - Total: 225
  Carol: Won (+100 points) - Total: 200
  Eve: Won (+100 points) - Total: 100

═══════════════════════════════════════════════════════════════════════
                           RANKINGS
═══════════════════════════════════════════════════════════════════════
Rank │ Player    │ Score   │ Games │ Wins │ Win Rate │ Avg/Game
─────┼───────────┼─────────┼───────┼──────┼──────────┼──────────
#1   │ Alice     │ 225     │ 3     │ 2    │ 66.7%    │ 75.0
#2   │ Carol     │ 200     │ 2     │ 2    │ 100.0%   │ 100.0
#3   │ Bob       │ 125     │ 2     │ 1    │ 50.0%    │ 62.5
#4   │ David     │ 100     │ 1     │ 1    │ 100.0%   │ 100.0
#4   │ Eve       │ 100     │ 1     │ 1    │ 100.0%   │ 100.0

(Note: David and Eve share rank #4 due to tied scores)

═══════════════════════════════════════════════════════════════════════
                          STATISTICS
═══════════════════════════════════════════════════════════════════════
Total Players:         5
Total Games Played:    9
Total Points Awarded:  750

Highest Scorer:        Alice (225 points)
Most Games Played:     Alice (3 games)
Best Win Rate:         Carol & David & Eve (100%)
Highest Avg/Game:      Carol, David, Eve (100.0)

Score Distribution:
  200+ points: 2 players (Alice, Carol)
  100-199:     3 players (Bob, David, Eve)
  0-99:        0 players

═══════════════════════════════════════════════════════════════════════
                      SCORE HISTORY: Alice
═══════════════════════════════════════════════════════════════════════
Game 1: +100 (Win)  - Running Total: 100
Game 2: +100 (Win)  - Running Total: 200
Game 3: +25 (Loss)  - Running Total: 225
─────────────────────────────────────────────────────────────────────────

Finding players with 100-200 points:
  Carol: 200 points
  Bob: 125 points
  David: 100 points
  Eve: 100 points

╚══════════════════════════════════════════════════════════════════════╝
```

**Code Requirements:**
- Use List<T> for all collections
- Implement custom sorting with IComparer or lambda
- Handle tied rankings properly
- Track score history per player
- Clean code with helper methods

---

## Evaluation Criteria

For each level, your code will be evaluated on:

1. **Correctness**: Do list operations work properly?
2. **Method Usage**: Are List<T> methods used appropriately?
3. **Edge Cases**: Handles empty lists, not found, etc.?
4. **Code Quality**: Clean, readable implementation?

**Passing**: All requirements must be met to pass each level.
**Failure**: Missing any requirement results in a retry at the same level.

---

*Complete all 9 levels to confirm mastery of List<T> Collections concepts.*

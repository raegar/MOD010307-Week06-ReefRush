# Reef Rush

The Week 6 practice project for **MOD010307 Mobile Game Programming**: collections and game systems in Unity 6.3.

Fish swim across a coral reef in waves. Tap a fish to catch it, catch the fish on the target card for a bonus, and don't let ten fish get away. At the end, put your initials on the saved high score table.

> **Practice, not assessment.** Reef Rush is a guided practice project. Use its *techniques* in your own microgames, but don't submit these scripts, or lightly altered copies of them, as Learning Outcome 3 evidence.

## Controls

| Input | Does |
|---|---|
| Tap (or click) a fish | Catch it |
| **Swarm +500** (Ready screen) | Add 500 fish to play with |
| **Start** | Clear the reef and start wave 1 |
| Initials + **Save** (game over) | Add your score to the high score table |
| **Replay** | A fresh game |

## The collections

| Collection | Where | What it holds | What happens to it |
|---|---|---|---|
| `FishInfo[] catalogue` | `FishSpawner` | every kind of fish: type, sprite, points | set in the Inspector |
| `List<Fish> allFish` | `FishSpawner` | every fish on the reef | `Add` on spawn; removed (looping backwards) when caught or when it swims away; its `Count` ends each wave |
| `List<FishType> targets` | `TargetCard` | the fish still to catch on the card | `Remove` when one is caught; dealt again when empty |
| `Image[] icons` | `TargetCard` | the card's icons | kept in step with `targets` by index |
| `Wave[] waves` | `WaveRunner` | each wave's fish count, interval and speed | set in the Inspector; played in order by a coroutine |
| `List<ScoreEntry> entries` | `HighScoreTable` | the high scores | `Add`, `Sort` highest first (with a tie-breaker), `RemoveRange` to keep 10, saved as JSON in `PlayerPrefs` |

## Scripts

| Folder | Scripts |
|---|---|
| `Assets/Scripts/Fish` | `Fish`, `FishType`, `FishInfo`, `FishSpawner`, `TapCatcher`, `SelfSwimmer` (stress test only) |
| `Assets/Scripts/Scores` | `ScoreKeeper`, `TargetCard`, `ScoreEntry`, `HighScoreTable` |
| `Assets/Scripts/Waves` | `Wave`, `WaveRunner` |
| `Assets/Scripts/Game` | `GameManager` (Ready → Playing → Over) |
| `Assets/Scripts/UI` | `FrameRateDisplay` |
| `Assets/Scripts/Drills` | `CollectionDrills` (Friday's exercises: these contain bugs on purpose) |

## Commit map

One commit per tutorial step, so you can compare your project with each step.

| Step | Commit message |
|---|---|
| — | Create Reef Rush project from the Universal 2D template |
| A0 | Set up the Reef scene: Kenney Fish Pack sprites, seabed, seaweed and a Fish prefab |
| A1 | Spawn fish with a for loop, each with a random sprite from an array |
| A2 | Keep every fish in a List, add a 500-fish Swarm button, and move them all in one loop |
| A3 | Catch fish by tapping, and remove fish that swim off the reef (looping backwards) |
| A4 | Add fish types with points, a ScoreKeeper, and a target card kept in a List |
| B1 | Send the fish in data-driven waves, using a Wave array and a coroutine |
| B2 | Add a high score table: sort descending with a tie-breaker, keep the top 10, save as JSON |
| B3 | Add a GameManager: Ready, Playing and Over states, escapes, and initials for the high score table |
| B4 ⭐ | Add a stress test: a frame rate display and a per-fish Update toggle for the Profiler |
| B5 | Document Reef Rush: collections, controls, commit map, manual tests and credits (this README) |
| Friday | Add Friday's collection drills |

## Manual tests

| # | Test | Expected |
|---|---|---|
| 1 | Open the game, press Swarm | 500 fish appear; the counter shows them |
| 2 | Press Start | The swarm is cleared, "Wave 1 of 4" shows, then fish swim in from both sides |
| 3 | Tap a fish | It disappears; the score goes up by its points |
| 4 | Tap a fish shown on the target card | Extra 25 points; its icon leaves the card |
| 5 | Catch all three targets | Extra 100 points; a new card is dealt |
| 6 | Tap empty water | Nothing happens |
| 7 | Let fish swim away | Escaped counts up; at 10 the game ends with "Too many got away!" |
| 8 | Clear all four waves | "Reef cleared!" |
| 9 | Type initials and press Save | The score appears in the right place, highlighted; only the top 10 are kept |
| 10 | Close the app and open it again | The high scores are still there |
| 11 | Press Replay | A fresh game: score 0, Escaped 0, Ready screen |
| 12 | Stress test: Swarm Size 5000, Use Manager Loop on, then off | Compare BehaviourUpdate in the Profiler (CPU Usage, Hierarchy view) |

## Credits

- Sprites: [Fish Pack](https://kenney.nl/assets/fish-pack) by Kenney (www.kenney.nl), licensed [CC0](https://creativecommons.org/publicdomain/zero/1.0/).
- Font: Unity's built-in LegacyRuntime font.

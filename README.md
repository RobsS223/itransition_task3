
# 🎲 Fair Random Dice Game Demonstration

This project implements a generalized **non-transitive dice game** using C#. It emphasizes **fair random generation** via cryptographically secure methods, using HMAC with SHA3‑256 to guarantee fairness and transparency.

---

## 🚀 Features

- **🎛️ Configurable Dice:**  
  Dice are customizable via command-line arguments. Provide configurations as comma-separated integers representing dice faces. A minimum of **3 dice** is required.

- **🔒 Fair Random Generation:**  
  A secure 256-bit key and random number generation combined with **HMAC SHA3‑256** ensures fairness. The computer commits to its choice, which is later revealed to verify fairness.

- **🖥️ Interactive CLI with Help:**  
  Supports dice selection, game exit, and help (`?`). Includes a neatly formatted ASCII table displaying win probabilities.

- **📦 Modular Design:**  
  Clearly separated responsibilities:
  - `Dice` & `DiceParser`: Dice abstraction and input parsing.
  - `FairRandomGenerator` & `FairProtocol`: Secure random generation and user interaction.
  - `Game`: Core game logic.
  - `ProbabilityCalculator` & `TablePrinter`: Probability calculation and display.

---

## 📌 Requirements

- **.NET 6/7/8 (or newer)**
- **NuGet Packages:**
  - [ConsoleTables](https://www.nuget.org/packages/ConsoleTables)
  - [SHA3.Net](https://www.nuget.org/packages/SHA3.Net)

---

## 💻 How to Run

### 1. Clone the Repository
```bash
git clone <repository_url>
cd <repository_directory>
```

### 2. Build and Run Examples

**Four Identical Dice:**
```bash
dotnet run 1,2,3,4,5,6 1,2,3,4,5,6 1,2,3,4,5,6 1,2,3,4,5,6
```

**Three Dice:**
```bash
dotnet run 2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7
```

### 3. Testing Incorrect Configurations

**No Dice Provided:**
```bash
dotnet run
```

**Only Two Dice Provided:**
```bash
dotnet run 1,2,3,4,5,6 7,8,9,10,11,12
```

**Invalid Dice Configuration:**
```bash
dotnet run 1,2,3,4,5 1,2,three,4,5,6 7,8,9,10,11,12
```

---

## 📖 How It Works

### Determining the First Move
- Computer commits to a random choice (0 or 1) using HMAC.
- User guesses (0, 1, `X` for exit, or `?` for help).
- Computer reveals the secret key and random choice. Result calculation:
```
(computerNumber + userInput) % 2
```

### Dice Selection
- User selects dice; type `?` for help and win probabilities.
- Computer selects optimal remaining dice automatically.

### Dice Rolls
- **Computer Roll:** Secure random number generated with HMAC.
- **User Roll:** Secure random number generated; user inputs a number.
- Both rolls verified using HMAC and revealed keys. Result calculation:
```
(computerNumber + userInput) % (number of faces)
```

### Result Comparison
- Compare final dice face values.
- Announce winner clearly.

---

## 📹 Video Demonstration

Check out the detailed [Video Demonstration](https://www.youtube.com/watch?v=3Dkv-bRjPGU), covering:

- Application with various dice configurations.
- Handling incorrect parameters.
- Using help functionality.
- Complete game sessions with output verification.

---

## 📧 Contact

**Robert Saakyan**  
📮 Email: [robs.s223@gmail.com](mailto:robs.s223@gmail.com)

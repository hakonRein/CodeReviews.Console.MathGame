/*
 * A simple math game that allows the user to select an operation and perform calculations.
 */

int menuChoice = 0;
int totalPoints = 0;
List<string> history = new List<string>();

string difficulty = "easy";
const int EASY_RANGE = 10;
const int DIFFICULT_RANGE = 100;
const int QUESTIONS_PER_GAME = 5;

// Used to store operations for the random operator option
string[] operations = { "+", "-", "*", "/" };

Random numberGenerator = new Random();

do
{
    DisplayMenu();
    while (!int.TryParse(Console.ReadLine(), out menuChoice) || menuChoice < 1 || menuChoice > 8)
    {
        Console.Write("Invalid input. Please enter a number between 1 and 8: ");
    }

    int num1;
    int num2;
    int correctAnswer;
    string operation = "";

    switch (menuChoice)
    {
        case 1:
            operation = "+";
            break;
        case 2:
            operation = "-";
            break;
        case 3:
            operation = "*";
            break;
        case 4:
            operation = "/";
            break;
        case 5:
            // Handle random operator
            operation = operations[numberGenerator.Next(0, operations.Length)];
            break;
        case 6:
            Console.WriteLine("\nHistory of calculations:");
            foreach (var record in history)
            {
                Console.WriteLine(record);
            }
            break;
        case 7:
            difficulty = difficulty == "easy" ? "difficult" : "easy";
            Console.WriteLine($"Changed difficulty to {difficulty}");
            break;
        case 8:
            Console.WriteLine("Exiting the game. Thank you for playing!");
            break;
    }

    // Do calculations only if the user selected a calculation operation (1-5)
    if (menuChoice >= 1 && menuChoice <= 5)
    { 
        for (int i = 1; i <= QUESTIONS_PER_GAME; i++)
        {
            num1 = numberGenerator.Next(0, difficulty == "easy" ? EASY_RANGE : DIFFICULT_RANGE);
            num2 = numberGenerator.Next(0, difficulty == "easy" ? EASY_RANGE : DIFFICULT_RANGE);

            // Ensure that division problems are valid (no division by zero and integer results)
            if (operation == "/")
            {
                num2 = numberGenerator.Next(1, difficulty == "easy" ? EASY_RANGE : DIFFICULT_RANGE);
                num1 = num2 * numberGenerator.Next(0, 10);
            }

            // Calculate the correct answer and check this against the user's guess
            correctAnswer = Calculate(num1, num2, operation);
            Console.Write($"\nQ{i}: {num1} {operation} {num2} = ");
            int userAnswer;
            while (!int.TryParse(Console.ReadLine(), out userAnswer))
            {
                Console.Write("Invalid input. Please enter a valid integer: ");
            }
            if (userAnswer == correctAnswer)
            {
                Console.WriteLine("Correct!");
                totalPoints++;
                history.Add($"{num1} {operation} {num2} = {correctAnswer} (Correct)");
            }
            else
            {
                Console.WriteLine($"Incorrect. The correct answer is {correctAnswer}.");
                history.Add($"{num1} {operation} {num2} = {userAnswer} (Incorrect - Correct answer: {correctAnswer})");
            }
            Console.WriteLine($"Total points: {totalPoints}");
        }
    }

} while (menuChoice != 8);


int Calculate(int num1, int num2, string operation)
{
    switch (operation)
    {
        case "+":
            return num1 + num2;
        case "-":
            return num1 - num2;
        case "*":
            return num1 * num2;
        case "/":
            return num1 / num2;
        default:
            throw new ArgumentException("Invalid operator");
    }
}

void DisplayMenu()
{
    Console.WriteLine("\nSelect operation:");
    Console.WriteLine("1. Addition");
    Console.WriteLine("2. Subtraction");
    Console.WriteLine("3. Multiplication");
    Console.WriteLine("4. Division");
    Console.WriteLine("5. Random operator");
    Console.WriteLine("6. Show history");
    Console.WriteLine("7. Change difficulty to " + (difficulty == "easy" ? "difficult" : "easy"));
    Console.WriteLine("8. Exit");
    Console.Write("Your choice (1-8): ");
}
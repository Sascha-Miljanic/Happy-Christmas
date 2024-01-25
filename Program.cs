namespace HappyChristmas;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "Mry Chrstms";
        Console.ForegroundColor = ConsoleColor.Gray;

        Queue<ConsoleColor> colours = new();
        colours.Enqueue(ConsoleColor.Red);
        colours.Enqueue(ConsoleColor.Green);
        colours.Enqueue(ConsoleColor.Blue);
        var getNextColour = () =>
        {
            var pop = colours.Dequeue();
            colours.Enqueue(pop);
            return pop;
        };

        string message = """
                                    \ /
                                  -->?y*?w<--
                                    /?no?w\
                                   /_\_\
                                  /_/_?n0?w_\
                                 /_?no?w_\_\_\
                                /_/_/_/_/?no?w\
                               /?n@?w\_\_\?n@?w\_\_\
                              /_/_/?no?w/_/_/_/_\
                             /_\_\_\_\_\?no?w\_\_\
                            /_/?n0?w/_/_/_?n0?w_/_/?n@?w/_\
                           /_\_\_\_\_\_\_\_\_\_\
                          /_/?no?w/_/_/?n@?w/_/_/?no?w/_/?n0?w/_\
                                   [___]
                         """;

        Dictionary<char, Action> charMap = new()
        {
            { 'n', () => Console.ForegroundColor = getNextColour() },
            { 'y', () => Console.ForegroundColor = ConsoleColor.Yellow },
            { 'w', () => Console.ForegroundColor = ConsoleColor.Gray },
            { 't', () => Console.Write("\t") }
        };

        while (true)
        {
            Console.SetCursorPosition(0,0);
            SmartPrintLine(message, '?', charMap);
            getNextColour();//this just shifts the colours by one position
            Thread.Sleep(500);
        }
    }



    static void SmartPrintLine(string message, char escapeChar, Dictionary<char, Action> specialCharacterToColourMapping)
    {
        for (int charIndex = 0; charIndex < message.Length; charIndex++)
        {
            if (message[charIndex] == escapeChar)
            {
                if (charIndex == message.Length - 1)//throw if escape characters the last character
                    throw new Exception($"Escape character '{escapeChar}' can't be the last character in {nameof(message)}");


                if (specialCharacterToColourMapping.TryGetValue(message[charIndex + 1], out var specialCharHandler))
                {
                    specialCharHandler();
                    charIndex += 1;
                    continue;
                }
                throw new Exception($"{message[charIndex + 1]} does not match any keys in {specialCharacterToColourMapping}");
            }
            Console.Write(message[charIndex]);
        }
    }
}
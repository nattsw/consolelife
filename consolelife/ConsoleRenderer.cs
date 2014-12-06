using System;

namespace consolelife
{
    public class ConsoleRenderer
    {
        Life _life;
        public ConsoleRenderer(Life life)
        {
            _life = life;
        }

        public string Render()
        {
            string[] lines = new string[_life.Rows];

            for (int i = 0; i < _life.Rows; i++)
            {
                lines[i] = RenderRow(i);
            }

            return String.Join("\n", lines);
        }

        string RenderRow(int row)
        {
            string returnString = "";

            foreach (bool b in _life.GetRow(row))
            {
                returnString += b ? "x" : " ";
            }

            return returnString;
        }
    }
}


using NUnit.Framework;
using System;

namespace consolelife
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestInitsWithDeadCells ()
		{
			Life life = new Life (2,2);
			Assert.AreEqual (false, life.IsAlive(0,0));
			Assert.AreEqual (false, life.IsAlive(0,1));
			Assert.AreEqual (false, life.IsAlive(1,0));
			Assert.AreEqual (false, life.IsAlive(1,1));
		}

		[Test()]
		public void TestSetAlive()
		{
			Life life = new Life(2, 2);
			life.SetCell(1, 1);
			Assert.AreEqual(true, life.IsAlive(1, 1));
		}

		[Test()]
		public void TestRuleOne()
		{
			//-------------------------------------
			// Alive surrounded by dead
			Life life = new Life(3, 3);
			life.SetCell(1, 1);

			life.Iterate();

			Assert.AreEqual(false, life.IsAlive(1, 1));

			//-------------------------------------
			// Two alive next to each other
			life.ResetCells();
			life.SetCell(1, 1);
			life.SetCell(1, 0);

			life.Iterate();

			Assert.AreEqual(false, life.IsAlive(1, 1));
			Assert.AreEqual(false, life.IsAlive(1, 0));

			//-------------------------------------
			// One alive in corner
			life.ResetCells();
			life.SetCell(0, 0);

			life.Iterate();

			Assert.AreEqual(false, life.IsAlive(0, 0));
		}

		[Test()]
		public void TestRuleTwo()
		{
			Life life = new Life(3, 3);
			//-------------------------------------
			// Middle one with two neighbors
			life.SetCell(1, 0);
			life.SetCell(1, 1);
			life.SetCell(1, 2);
			life.Iterate();
			// Middle one lives
			Assert.AreEqual(true, life.IsAlive(1, 1));

			//-------------------------------------
			// Middle one with three neighbors
			life.ResetCells();
			life.SetCell(0, 1);
			life.SetCell(1, 0);
			life.SetCell(1, 1);
			life.SetCell(1, 2);

			life.Iterate();

			// Middle one lives
			Assert.AreEqual(true, life.IsAlive(1, 1));
		}

		[Test()]
		public void TestRuleThree()
		{
			Life life = new Life(3, 3);
			//-------------------------------------
			// Middle one with four neighbors
			life.ResetCells();
			life.SetCell(0, 1);
			life.SetCell(1, 0);
			life.SetCell(1, 1);
			life.SetCell(1, 2);
			life.SetCell(2, 1);

			life.Iterate();

			// Middle one dies
			Assert.AreEqual(false, life.IsAlive(1, 1));
		}

		[Test()]
		public void TestRuleFour()
		{
			Life life = new Life(3, 3);
			//-------------------------------------
			// Middle one dead, with 3 neighbors
			life.SetCell(0, 1);
			life.SetCell(1, 0);
			life.SetCell(1, 2);

			life.Iterate();

			// Middle one becomes alive
			Assert.AreEqual(true, life.IsAlive(1, 1));
		}

		[Test()]
		public void TestBlinker()
		{
			Life life = new Life(3, 3);
			//-------------------------------------
			// Horizontal line in middle row
			life.SetCell(1, 0);
			life.SetCell(1, 1);
			life.SetCell(1, 2);

			life.Iterate();

			// Should have become vertical line in middle col
			Assert.AreEqual(true, life.IsAlive(1, 1));

			Assert.AreEqual(false, life.IsAlive(1, 0));
			Assert.AreEqual(false, life.IsAlive(1, 2));

			Assert.AreEqual(true, life.IsAlive(0, 1));
			Assert.AreEqual(true, life.IsAlive(2, 1));

			life.Iterate();

			// Should have become horizontal line in middle row
			Assert.AreEqual(true, life.IsAlive(1, 1));

			Assert.AreEqual(true, life.IsAlive(1, 0));
			Assert.AreEqual(true, life.IsAlive(1, 2));

			Assert.AreEqual(false, life.IsAlive(0, 1));
			Assert.AreEqual(false, life.IsAlive(2, 1));
        }

        [Test()]
        public void TestGetRow()
        {
            Life life = new Life(5, 5);

            life.SetCell(0, 1);
            life.SetCell(0, 3);

            Assert.AreEqual(new bool[]{false, true, false, true, false}, life.GetRow(0));
        }

        [Test()]
        public void TestRender()
        {
            Life life = new Life(2, 5);
            ConsoleRenderer consoleRenderer = new ConsoleRenderer(life);

            life.SetCell(0, 1);
            life.SetCell(0, 3);
            life.SetCell(1, 0);
            life.SetCell(1, 2);
            life.SetCell(1, 4);

            string expected_first_row_string = " x x ";
            string expected_second_row_string = "x x x";

            string expected_string = expected_first_row_string + "\n" + expected_second_row_string;
            string actual_string = consoleRenderer.Render();

            Assert.AreEqual(expected_string, actual_string);
        }
	}
}


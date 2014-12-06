using System;

namespace consolelife
{
	public class Life
	{
		private int _rows, _cols;
		private bool[,] _cells;
		public Life (int rows, int cols)
		{
			_rows = rows;
			_cols = cols;
			_cells = new bool[_rows, _cols];
			ResetCells();
		}

        public int Rows
        {
            get { return _rows; }
        }
			
        public void ResetCells()
        {
            ForLoop(0,_rows,0,_cols, (a,b) => 
                { 
                    _cells[a, b] = false; 
                });
		}

		public bool IsAlive(int row, int col)
		{
			return _cells [row, col];
		}

        public bool[] GetRow(int row)
        {
            bool[] row_result = new bool[_cols];
            for (int col = 0; col < _cols; col++)
            {
                row_result[col] = IsAlive(row, col);
            }
            return row_result;
        }

		public void SetCell(int row, int col)
		{
			_cells[row, col] = true;
		}

		public void Iterate()
		{
			int[,] neighbor_counts = NeighborCounts();

			for (int i = 0; i < _rows; i++)
			{
				for (int j = 0; j < _cols; j++)
				{
					int numberOfNeighbors = neighbor_counts[i, j];

					if (IsAlive(i, j))
					{
						// Rule 1: kill underpopulated
						if (numberOfNeighbors < 2) _cells[i, j] = false;

						// Rule 2 says to leave alive cells with 2 or 3 neighbors alone, so we will...

						// Rule 3: kill overpopulated
						else if (numberOfNeighbors > 3) _cells[i, j] = false; 
					}
					else 
					{
						// Rule 4: resurrect if dead and has 3 neighbors
						if (numberOfNeighbors == 3) _cells[i, j] = true; 
					}
				}
			}
		}

		int[,] NeighborCounts()
		{
			int[,] neighbor_cells = new int[_rows,_cols];
			for (int i = 0; i < _rows; i++)
			{
				for (int j = 0; j < _cols; j++)
				{
					neighbor_cells[i, j] = GetNumberOfNeighbors(i,j);
				}
			}
			return neighbor_cells;
		}

		int GetNumberOfNeighbors(int row, int col)
		{
			int neighbors = 0;
			for (int curr_row = row - 1; curr_row <= row + 1; curr_row++)
			{
				for (int curr_col = col-1; curr_col <= col + 1; curr_col++)
				{
					bool isSelf = (curr_row == row && curr_col == col);
					if (isSelf) continue;

					if (IsOutOfBounds(curr_row, curr_col)) continue;

					if (IsAlive(curr_row, curr_col)) neighbors++;
				}
			}
			return neighbors;
		}

		bool IsOutOfBounds(int row, int col) 
		{
			return ((row < 0) || (row >= _rows) || (col < 0) || (col >= _cols));
		}

		void ForLoop(int x1, int x2, int y1, int y2, Action<int, int> a) {
			for (int i = x1; i < x2; i++)
			{
				for (int j = y1; j < y2; j++)
				{
					a(i,j);
				}
			}
		}
	}
}
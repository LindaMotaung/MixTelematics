using System;
using System.Collections.Generic;

namespace MixTelematics
{
    public class Grid
    {
        private readonly Dictionary<int, List<Coordinates>> _cellCoordinatesMapping;

        public Grid()
        {
            _cellCoordinatesMapping = new Dictionary<int, List<Coordinates>>();
        }

        public void AddCoordinatesToCell(int cellId, Coordinates coordinate)
        {
            if (!_cellCoordinatesMapping.ContainsKey(cellId))
            {
                _cellCoordinatesMapping[cellId] = new List<Coordinates>();
            }

            _cellCoordinatesMapping[cellId].Add(coordinate);
        }

        public virtual List<Coordinates> GetCoordinatesInCell(int cellId)
        {
            return _cellCoordinatesMapping.TryGetValue(cellId, out var coordinates) ? coordinates : new List<Coordinates>();
        }

        public static int GetCellId(double latitude, double longitude)
        {
            const int numRows = 3; //The number of rows in the grid
            const int numColumns = 3; //The number of columns in the grid

            var cellId = (int)(latitude * numRows) * numColumns + (int)(longitude * numColumns);

            return cellId;
        }

        public static bool IsInSameOrAdjacentCell(Coordinates coordinate, Vehicle vehicle, Grid grid)
        {
            var coordinateCellId = GetCellId(coordinate.Latitude, coordinate.Longitude);
            var vehicleCellId = GetCellId(vehicle.Latitude, vehicle.Longitude);

            // Check if the person is in the same cell or any of the adjacent cells
            var isAdjacent = Math.Abs(coordinateCellId - vehicleCellId) <= 1;

            return isAdjacent;
        }
    }
}

using NUnit.Framework;
using UnityEngine;
using _Scripts;

[TestFixture]
public class CellTests
{
    private Cell _cell;

    [SetUp]
    public void SetUp()
    {
        // Create a new Cell object before each test
        _cell = new GameObject().AddComponent<Cell>();
    }

    [Test]
    public void SetState_SetsStateCorrectly()
    {
        // Arrange
        CellState expectedState = CellState.Revealed;

        // Act
        _cell.SetState(expectedState);

        // Assert
        Assert.AreEqual(expectedState, _cell._currentState);
    }
}
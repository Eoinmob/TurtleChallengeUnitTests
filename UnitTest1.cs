using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TurtleChallenge;

namespace TurtleChallengeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLoadSettingsFromFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Settings settings = TurtleChallengeMain.LoadSettingsFromFile(path + "test-game-settings.json");

            Assert.AreEqual(settings.N, 10, "Incorrect N value loaded from file");
            Assert.AreEqual(settings.M, 10, "Incorrect M value loaded from file");
            Assert.AreEqual(settings.startDir, 0, "Incorrect startDir value loaded from file");
            Assert.AreEqual(settings.startPos[0], 5, "Incorrect startDir x value loaded from file");
            Assert.AreEqual(settings.startPos[1], 5, "Incorrect startDir y value loaded from file");
        }

        [TestMethod]
        public void TestLoadMovesFromFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            List<int[]> sequences = TurtleChallengeMain.LoadMovesFromFile(path + "test-sequences.json");

            List<int[]> testSequences = new List<int[]>() { new int[] { 0, 0, 0, 0, 0, 0 }, new int[] { 1, 1, 1, 0, 0, 0 }, new int[] { 0, 0, 1, 1, 1, 0, 0 }, new int[] { 1, 1, 1, 0 } };

            CollectionAssert.AreEqual(sequences[0], testSequences[0], "Sequence 0 loading incorrectly");
            CollectionAssert.AreEqual(sequences[1], testSequences[1], "Sequence 0 loading incorrectly");
            CollectionAssert.AreEqual(sequences[2], testSequences[2], "Sequence 0 loading incorrectly");
        }

        [TestMethod]
        public void TestProcessMoveSequence_OutOfBounds()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Settings settings = TurtleChallengeMain.LoadSettingsFromFile(path + "test-game-settings.json");
            Board board = new Board(settings);

            int[] testSequence = new int[] { 0, 0, 0, 0, 0, 0 };

            string result = TurtleChallengeMain.ProcessMoveSequence(testSequence, board);

            Assert.AreEqual(result, TurtleChallengeMain.OUTOFBOUNDS, "Turtle not registering as out of bounds");
        }

        [TestMethod]
        public void TestProcessMoveSequence_Success()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Settings settings = TurtleChallengeMain.LoadSettingsFromFile(path + "test-game-settings.json");
            Board board = new Board(settings);

            int[] testSequence = new int[] { 1, 1, 1, 0, 0, 0 };

            string result = TurtleChallengeMain.ProcessMoveSequence(testSequence, board);

            Assert.AreEqual(result, TurtleChallengeMain.SUCCESS, "Turtle not registering as reaching exit");
        }

        [TestMethod]
        public void TestProcessMoveSequence_Failure()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Settings settings = TurtleChallengeMain.LoadSettingsFromFile(path + "test-game-settings.json");
            Board board = new Board(settings);

            int[] testSequence = new int[] { 0, 0, 1, 1, 1, 0, 0 };

            string result = TurtleChallengeMain.ProcessMoveSequence(testSequence, board);

            Assert.AreEqual(result, TurtleChallengeMain.FAILURE, "Turtle not registering as hitting mine");
        }

        [TestMethod]
        public void TestProcessMoveSequence_Incomplete()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Settings settings = TurtleChallengeMain.LoadSettingsFromFile(path + "test-game-settings.json");
            Board board = new Board(settings);

            int[] testSequence = new int[] { 1, 1, 1, 0};

            string result = TurtleChallengeMain.ProcessMoveSequence(testSequence, board);

            Assert.AreEqual(result, TurtleChallengeMain.INCOMPLETE, "Turtle not registering as still in field");
        }

        [TestMethod]
        public void TestBoard_Setup()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Settings settings = TurtleChallengeMain.LoadSettingsFromFile(path + "test-game-settings.json");
            Board board = new Board(settings);

            Assert.AreEqual(board.N, 10, "Incorrect N value loaded from file");
            Assert.AreEqual(board.M, 10, "Incorrect M value loaded from file");
            Assert.AreEqual(board.StartDir, 0, "Incorrect value loaded from file");
            Assert.AreEqual(board.StartPos[0], 5, "Incorrect startDir x value loaded from file");
            Assert.AreEqual(board.StartPos[1], 5, "Incorrect startDir y value loaded from file");
        }

        public void TestBoard_Populate()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Settings settings = TurtleChallengeMain.LoadSettingsFromFile(path + "test-game-settings.json");
            Board board = new Board(settings);

            Assert.AreEqual(board.Tiles[2, 3], Board.MINE, "Incorrect value at sampled tile");
            Assert.AreEqual(board.Tiles[4, 3], Board.MINE, "Incorrect value at sampled tile");
            Assert.AreEqual(board.Tiles[2, 5], Board.EXIT, "Incorrect value at sampled tile");
        }

        [TestMethod]
        public void TestMoveTurtle_North()
        {
            Turtle.Pos = new int[] { 7, 7 };
            Turtle.Dir = 0;
            Turtle.MoveTurtle();

            Assert.AreEqual(Turtle.Pos[0], 7, "Incorrect x position after move");
            Assert.AreEqual(Turtle.Pos[1], 6, "Incorrect y position after move");
        }

        [TestMethod]
        public void TestMoveTurtle_East()
        {
            Turtle.Pos = new int[] { 7, 7 };
            Turtle.Dir = 1;
            Turtle.MoveTurtle();

            Assert.AreEqual(Turtle.Pos[0], 8, "Incorrect x position after move");
            Assert.AreEqual(Turtle.Pos[1], 7, "Incorrect y position after move");
        }

        [TestMethod]
        public void TestMoveTurtle_South()
        {
            Turtle.Pos = new int[] { 7, 7 };
            Turtle.Dir = 2;
            Turtle.MoveTurtle();

            Assert.AreEqual(Turtle.Pos[0], 7, "Incorrect x position after move");
            Assert.AreEqual(Turtle.Pos[1], 8, "Incorrect y position after move");
        }

        [TestMethod]
        public void TestMoveTurtle_West()
        {
            Turtle.Pos = new int[] { 7, 7 };
            Turtle.Dir = 3;
            Turtle.MoveTurtle();

            Assert.AreEqual(Turtle.Pos[0], 6, "Incorrect x position after move");
            Assert.AreEqual(Turtle.Pos[1], 7, "Incorrect y position after move");
        }

        [TestMethod]
        public void TestRotateTurtle()
        {
            Turtle.Dir = 3;

            Turtle.RotateTurtle();

            Assert.AreEqual(Turtle.Dir, 0, "Incorrect direction after rotation");
        }
    }
}

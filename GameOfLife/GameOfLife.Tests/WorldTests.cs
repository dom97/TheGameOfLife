using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Tests
{
    [TestClass]
    public class WorldTests
    {
        [TestMethod]
        public void AnyLiveCellWithFewerThanTwoLiveNeighboursDies()
        {
            var world = new World(4, 4);
            world.BringToLife(1, 1);
            world.Tick();

            for (var i = 1; i <= 4; i++)
                for (var j = 1; j <= 4; j++)
                    Assert.IsFalse(world.IsAlive(i, j));
        }

        [TestMethod]
        public void AnyLiveCellWithTwoLiveNeighborsLives()
        {
            var world = new World(4, 4);
            world.BringToLife(1, 1);
            world.BringToLife(2, 1);
            world.BringToLife(1, 2);
            world.Tick();

            Assert.IsTrue(world.IsAlive(1, 1));
            Assert.IsTrue(world.IsAlive(2, 1));
            Assert.IsTrue(world.IsAlive(1, 2));
        }

        [TestMethod]
        public void AnyLiveCellWithThreeLiveNeighborsLives()
        {
            var world = new World(4, 4);
            world.BringToLife(1, 1);
            world.BringToLife(2, 1);
            world.BringToLife(1, 2);
            world.BringToLife(2, 2);
            world.Tick();

            Assert.IsTrue(world.IsAlive(1, 1));
            Assert.IsTrue(world.IsAlive(2, 1));
            Assert.IsTrue(world.IsAlive(1, 2));
            Assert.IsTrue(world.IsAlive(2, 2));
        }

        [TestMethod]
        public void AnyLiveCellWithMoreThanThreeLiveNeighborsDies()
        {
            var world = new World(4, 4);
            world.BringToLife(2, 2);
            world.BringToLife(1, 1);
            world.BringToLife(1, 2);
            world.BringToLife(2, 3);
            world.BringToLife(3, 2);
            world.Tick();

            Assert.IsFalse(world.IsAlive(2, 2));
            Assert.IsTrue(world.IsAlive(1, 1));
            Assert.IsTrue(world.IsAlive(1, 2));
            Assert.IsTrue(world.IsAlive(2, 3));
            Assert.IsTrue(world.IsAlive(3, 2));
        }

        [TestMethod]
        public void AnyDeadCellWithExactlyThreeLiveNeighborsBecomesALiveCell()
        {
            var world = new World(4, 4);
            world.BringToLife(1, 1);
            world.BringToLife(1, 2);
            world.BringToLife(2, 3);
            world.Tick();

            Assert.IsTrue(world.IsAlive(2, 2));
            Assert.IsFalse(world.IsAlive(1, 1));
            Assert.IsTrue(world.IsAlive(1, 2));
            Assert.IsFalse(world.IsAlive(2, 3));
        }
    }
}

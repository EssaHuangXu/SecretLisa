using NUnit.Framework;
using SecretLisa;
using UnityEngine;

namespace EditorTests
{
    
    public class Test
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Test_IsInFront()
        {
            var observer = new Entity(Vector3.zero, Vector3.right);
            var target = new Entity(Vector3.zero, Vector3.right);
            Assert.IsTrue(FrontDetectUtils.IsInFront(observer, target), $"{target} is not in front of {observer}");

            var target2 = new Entity(Vector3.back, Vector3.right);
            Assert.IsFalse(FrontDetectUtils.IsInFront(observer, target2), $"{target} is not in front of {observer}");
            
            var target3 = new Entity(new Vector3(1, 1, 0), Vector3.right);
            Assert.IsTrue(FrontDetectUtils.IsInFront(observer, target3), $"{target} is not in front of {observer}");
        }

        [Test]
        public void Test_GameStateSwitch()
        {
            var manager = new GameStateManager();
            Assert.IsTrue(manager.CurrentGameState is GameStateLobby, $"{manager.CurrentGameState.GetType()} is not GameStateLobby");
            
            manager.ChangeGameState(new GameStateEventBattleStart());
            Assert.IsTrue(manager.CurrentGameState is GameStateBattle, $"{manager.CurrentGameState.GetType()} is not GameStateBattle");
            
            manager.ChangeGameState(new GameStateEventBattleEnd());
            Assert.IsTrue(manager.CurrentGameState is GameStateBattleResult, $"{manager.CurrentGameState.GetType()} is not GameStateBattleResult");
            
            manager.ChangeGameState(new GameStateEventExitBattle());
            Assert.IsTrue(manager.CurrentGameState is GameStateLobby, $"{manager.CurrentGameState.GetType()} is not GameStateLobby");
        }
    }
}
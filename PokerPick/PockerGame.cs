using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerPick
{
    /// <summary>
    /// 游戏规则
    /// </summary>
    public class PockerGame
    {
       /// <summary>
       /// 每行剩余的火柴数量，行号从0开始
       /// </summary>
        public int[] PockerStates ;

    
        public List<GameRecord> Records = new List<GameRecord>();

        /// <summary>
        /// 当前轮到谁操作。1,2
        /// </summary>
        public int UserTurn;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PockerGame()
        {

            ResetGame();
        }

        /// <summary>
        /// 重置游戏
        /// </summary>
        public void ResetGame()
        {
            PockerStates = new int[] { 3, 5, 7 };
            UserTurn = 1;
           
        }

        /// <summary>
        /// 玩家执行一步，捡起哪一行的几个火柴
        /// </summary>
        /// <param name="userTurn"></param>
        /// <param name="rowNum"></param>
        /// <param name="pickCount"></param>
        /// <returns>是否允许执行</returns>
        public bool Step(int rowNum,int pickCount)
        {
            //检查条件
            if (rowNum >= 3)//行号小于3
                return false;
            if (pickCount < 1)//捡起数量要大于1
                return false;
            if (PockerStates[rowNum] <pickCount)//捡起数量要不大于现有数量
                return false;
            //更新火柴数
            PockerStates[rowNum] -= pickCount;

            //记录本步记录
            var record = new GameRecord() { UserTurn = UserTurn, RowNumber = rowNum, PickCount = pickCount };
            record.PockerStates = PockerStates.ToArray();
            Records.Add(record);

            //更新下一步该哪个玩家
            UserTurn = UserTurn + 1;
            if (UserTurn > 2)
                UserTurn = 1;
            return true;
        }


        public bool CheckEnd()
        {
            var end = PockerStates.All(p => p == 0);
            return end;
        }
    }
}

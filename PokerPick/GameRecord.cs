using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerPick
{
    /// <summary>
    /// 游戏步骤记录数据Model
    /// </summary>
    public class GameRecord
    {
        /// <summary>
        /// 用户序号
        /// </summary>
        public int UserTurn;
        /// <summary>
        /// 此步捡起的火柴行号
        /// </summary>
        public int RowNumber;
        /// <summary>
        /// 此步捡起的火柴数量
        /// </summary>

        public int PickCount;
        /// <summary>
        /// 此步的结局状态
        /// </summary>
        public int[] PockerStates;
    }

}

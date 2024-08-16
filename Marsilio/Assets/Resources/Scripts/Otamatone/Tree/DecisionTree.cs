using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Otamatone;

namespace Otamatone.Tree
{
    public delegate bool TreeCondition();

    public class DecisionTree : GeneralBehaviour
    {
        private TreeCondition condition;
        private DecisionTree Left { get; set; }

        private DecisionTree Right { get; set; }

        public DecisionTree(TreeCondition condition,DecisionTree left,DecisionTree right)
        {
            this.condition = condition;
            this.Left= left;
            this.Right= right;
        }

        public override IReasonable Reason()
        {
            if (condition())
                return Left.Reason();
            else return Right.Reason();
        }
    }
}

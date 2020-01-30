using UnityEngine;
using Vexe.Runtime.Types;

namespace SimpleInteractionSystem
{
    public abstract class NegatableCondition : BaseBehaviour, ICondition
    {
        #region | Fields | ************************************************************************

        [SerializeField]
        [Comment("A value indicating whether this condition should be negated.", true)]
        private bool _negate;

        #endregion

        #region | Abstract Methods | **************************************************************

        /// <summary>Returns a value indicating whether this <see cref="NegatableCondition"/> s full filled.</summary>
        /// <returns>
        /// <c>true</c> if this <see cref="NegatableCondition"/> is full filled; otherwise <c>false</c>.
        /// </returns>
        public abstract bool IsFullFilled();

        #endregion

        #region | Interface Implementations | *****************************************************

        #region | Implementation of ICondition | 

        /// <summary>Returns a value indicating whether this <see cref="ICondition"/> s full filled.</summary>
        /// <returns><c>true</c> if this <see cref="ICondition"/> is full filled; otherwise <c>false</c>.</returns>
        bool ICondition.IsFullFilled()
        {
            if (_negate)
            {
                return !IsFullFilled();
            }

            return IsFullFilled();
        }

        #endregion

        #endregion
    }
}
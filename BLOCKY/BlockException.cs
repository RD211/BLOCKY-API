
namespace BlockyAPI.BLOCKY
{
    public class BlockException
    {
        #region Variables
        public string errorMessage;
        public Block sender;
        #endregion

        #region Constructor
        public BlockException(Block block, string error)
        {
            this.errorMessage = error;
            this.sender = block;
        }
        #endregion

        #region Set exception flag on block
        public void SetFlagOnBlock() => this.sender.SetException(this);
        #endregion
    }
}

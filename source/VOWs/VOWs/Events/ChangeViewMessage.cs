namespace VOWs.Events
{
    public class ChangeViewMessage
    {
        /// <summary>
        /// The <c>IsExplicit</c> parameter indicates whether the event is calling explicity for
        /// a change to a certain view based off it's id, or calling for a toggle between <c>Id</c> 
        /// and <c>OtherId</c>.
        /// </summary>
        public bool IsExplicit { get; private set; }
        /// <summary>
        /// The <c>Id</c> parameter is always populated by an id, which may be the explicit id of
        /// the view to switch to, or the id of a view to toggle between.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// The <c>OtherId</c> parameter will contain the id of the other view if <c>IsExplicit</c>
        /// is false. Otherwise, it will likely be <c>null</c>.
        /// </summary>
        public int? OtherId { get; private set; }

        /// <summary>
        /// A constructor for <c>ChangeViewMessage</c> that initialises this event as an explicit
        /// view change with a specified id.
        /// </summary>
        /// <param name="id">The ID of the view (see <c>MainViewModel</c> for these).</param>
        public ChangeViewMessage(int id)
        {
            Id = id;
            IsExplicit = true;
        }
        /// <summary>
        /// A constructor for <c>ChangeViewMessage</c> that initialises this event as a toggle
        /// view change with a specified id and another to switch back and fourth between.
        /// </summary>
        /// <param name="id">The ID of the view (see <c>MainViewModel</c> for these).</param>
        /// <param name="otherId">The other ID of a view to switch back and fourth from (see <c>MainViewModel</c> for these).</param>
        public ChangeViewMessage(int id, int otherId)
        {
            Id = id;
            OtherId = otherId;
            IsExplicit = false;
        }
    }
}

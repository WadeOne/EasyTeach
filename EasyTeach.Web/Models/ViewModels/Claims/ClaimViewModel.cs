namespace EasyTeach.Web.Models.ViewModels.Claims
{
    public sealed class ClaimViewModel
    {
        private bool Equals(ClaimViewModel other)
        {
            return string.Equals(Operation, other.Operation) && string.Equals(Resource, other.Resource);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Operation != null ? Operation.GetHashCode() : 0)*397) ^ (Resource != null ? Resource.GetHashCode() : 0);
            }
        }

        public string Operation { get; set; }

        public string Resource { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ClaimViewModel && Equals((ClaimViewModel)obj);
        }
    }
}
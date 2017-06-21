
namespace Nop.Core.Configuration
{
    /// <summary>
    /// Represents startup hosting configuration parameters
    /// </summary>
    public partial class HostingConfig
    {
        /// <summary>
        /// Gets or sets custom forwarded HTTP header (e.g. CF-Connecting-IP, X-FORWARDED-PROTO, etc)
        /// </summary>
        public string ForwardedHttpHeader { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether to use HTTP_X_FORWARDED_PROTO; set null if not specified
        /// </summary>
        public bool? UseHttpXForwardedProto { get; set; }
    }
}
using Piranha.AttributeBuilder;
using Piranha.Models;

namespace NorthwindCms.Models
{
    [PostType(Title = "Standard post")]
    public class StandardPost  : Post<StandardPost>
    {
    }
}
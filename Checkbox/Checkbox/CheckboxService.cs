using Microsoft.Extensions.Options;
using System.Collections;

namespace Checkbox
{ 
    public class CheckboxService
    {
        public BitArray checkboxes {  get; set; }

        public CheckboxService(IOptions<CheckboxSizeOption> options)
        {
            checkboxes = new BitArray(options.Value.Size);
        }
    }
}

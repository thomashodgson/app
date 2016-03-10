using System;

namespace models
{
    public class MessageData 
    {
        public string Name { get; set; }
        public string RespondedHelloMessage { get; set; }
        public ErrorReport Error { get; set; }
        public Guid Guid { get; set; }
    }
}
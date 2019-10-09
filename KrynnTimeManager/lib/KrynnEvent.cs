using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrynnTimeManager.lib
{
  [Serializable]
  class KrynnEvent
  {
    public KrynnEvent(KrynnDateTime eventDT, KrynnDateTime currentDT, string name, string description)
    {
      this.DateTime = eventDT;
      this.SecondsLeft = currentDT.SecondsUntil(this.DateTime);
      this.Name = name;
      this.Description = description;
    }

    public KrynnDateTime DateTime { get; private set; }
    public int SecondsLeft { get; private set; }
    public String Name { get; private set; }
    public String Description { get; private set; }

    public bool UpdateTime(KrynnDateTime currentDT)
    {
      this.SecondsLeft = currentDT.SecondsUntil(this.DateTime);
      if (SecondsLeft <= 0)
        return true;
      else
        return false;
    }
  }
}

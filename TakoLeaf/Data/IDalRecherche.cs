using System;
namespace TakoLeaf.Data
{
    public interface IDalRecherche : IDisposable
    {
        bool EstAmi(int id1, int id2);
    }
}

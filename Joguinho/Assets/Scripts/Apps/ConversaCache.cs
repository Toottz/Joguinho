using System.Collections.Generic;

public static class ConversaCache
{
    private static HashSet<string> conversasSalvas = new HashSet<string>();
    public static void Salvar(string id)
    {
        conversasSalvas.Add(id);
    }

    public static bool FoiSalva(string id)
    {
        return conversasSalvas.Contains(id);
    }
}
using System.Collections.ObjectModel;
using System.Linq;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public class RefreshProgressItem
{
    public RefreshProgressItem()
    {
        ProgressItemVMs = new ObservableCollection<ProgressItemViewModel>();
    }

    /// <summary>
    /// Die DataSource der ListView im Frontend
    /// </summary>
    public ObservableCollection<ProgressItemViewModel> ProgressItemVMs { get; set; }

    public void RefreshSingleUpdateProgressItem(ProgressItem updatedProgressItem)
    {
        // *********************************************************
        // *********************************************************
        // Diese Methode aktualisiert einen einzelnen Eintrag in der
        // Liste der zu aktualisierenden Tabellen.
        // (Die Liste am rechten Bildschirmrand (Stammdaten aktualisieren))
        // Die Liste ist über Databinding mit ProgressItemVMs verbunden
        // Wir aktualisieren "live" genau einen Eintrag in der Collection
        // *********************************************************
        // *********************************************************

        ProgressItemViewModel progressItemVM;

        // *********************************************************
        // Hole aus der Liste ProgressItemVMs das ProgressItemViewModel mit
        // dem übergebenen Tabellennamen, wenn es schon enthalten ist
        // *********************************************************
        progressItemVM = ProgressItemVMs.FirstOrDefault(s => s.TableName == updatedProgressItem.LocalizedTableName);

        // *********************************************************
        // Wenn das ProgressItemViewModel nicht enthalten war,
        // dann erzeuge eines, setze den Tabellenname und füge es
        // der Collection hinzu
        // *********************************************************
        if (progressItemVM == null)
        {
            progressItemVM = new ProgressItemViewModel
            {
                TableName = updatedProgressItem.LocalizedTableName
            };

            if (!string.IsNullOrEmpty(progressItemVM.TableName))
                ProgressItemVMs.Add(progressItemVM);
        }

        // *********************************************************
        // Aktualisiere die Eigenschaften des gefundenen oder neu
        // hinzugefügten ProgressItemViewModel mit den Werten
        // aus dem übergebenen (updated) ProgressItem
        // *********************************************************

        progressItemVM.Changed = updatedProgressItem.Changed;
        progressItemVM.CurrentBaseDataUpdateState = updatedProgressItem.CurrentBaseDataUpdateState;
        progressItemVM.LastUpdate = updatedProgressItem.LastUpdate;
        progressItemVM.ProgressText = updatedProgressItem.ProgressText;
        progressItemVM.TotalChanges = updatedProgressItem.TotalChanges;
    }
}
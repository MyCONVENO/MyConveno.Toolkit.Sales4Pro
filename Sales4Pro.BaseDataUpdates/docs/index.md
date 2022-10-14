Aktualisieren der Stammdaten am Client

Es wird eine ObservableCollection **ProgressItemVMs** vom Typ ***ProgressItemViewModel*** erstellt 



Der Befehl **CheckSyncStatusOffline**

1.  ***GetUpdateTablesWithDefaultValues***
    Hole eine Liste vom Typ "ProgressItem", die alle Tabellen enthält, die zum Download vorgesehen sind.
2.  ***RefreshUpdateProgressItem*** 
    Gehe durch die einzelnen ProgressItems und
3.  
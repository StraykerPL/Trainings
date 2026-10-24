namespace OrdersManager.Constants
{
    internal static class MessagesConstants
    {
        public const string WelcomeMessage = "Witamy - OrdersManager";
        public const string OptionsMessage = "\nOpcje działania:\n1. Dodaj produkt do koszyka,\n2. Usuń produkt z koszyka,\n3. Wyświetl stan koszyka,\n4. Zrealizuj zamówienie z koszyka,\n5. Wyświetl zamówienia,\n6. Zakończ program,\n\nProszę wybrać numer: ";
        public const string AddProductNameMessage = "Podaj nazwę produktu:";
        public const string AddProductDescriptionMessage = "Podaj opis produktu:";
        public const string AddProductPriceMessage = "Podaj cenę produktu:";
        public const string ProvideProductIdMessage = "Podaj ID produktu:";
        public const string BasketEmptyMessage = "Koszyk jest pusty.";
        public const string BasketValueMessage = "Wartość koszyka: {0}";
        public const string ProvideShippingAddressMessage = "Podaj adres dostawy:";
        public const string WrongImputDataMessage = "Błędne dane wejściowe.";
        public const string NoOrdersToDisplayMessage = "Brak zamówień do wyświetlenia.";
        public const string NoDataProvidedForProductMessage = "Błąd: nazwa lub opis produktu są puste.";
        public const string NoIdProvidedMessage = "Błąd: nie podano ID.";
        public const string NoDeliveryAddressProvidedMessage = "Błąd: nie podano adresu dostawy.";
        public const string NoProductsInBasketForOrderingMessage = "Błąd: nie można utworzyć zamówienia z pustym koszykiem.";
        public const string DoubleConvertionErrorMessage = "Błąd: podaj prawidłową liczbę z przecinkiem.";
        public const string OrderFinalizationErrorMessage = "Błąd: nie udało się ukończyć zamówienia. Sprawdź dane i spróbuj ponownie.";
        public const string OrderFinalizationSuccessMessage = "Pomyślnie złożono zamówienie. Rozpoczęto pakowanie. Zamówienie zostanie wysłane po zapakowaniu, prosimy sprawdzać status.";
    }
}
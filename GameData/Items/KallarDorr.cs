using TextAdventure.Concepts;
using TextAdventure.Concepts.Implementations;
using TextAdventure.Language.Concepts;
using TextAdventure.Language.Swedish;

namespace TextAdventure.GameData.Items;

public class KallarDorr : SwedishAbstractItem
{
    public override INoun Name => new SweNoun("källardörr", Genus.Utrum);

    public override IEnumerable<INoun> Synonyms => 
    [
        new SweNoun("trädörr", Genus.Utrum),
        new SweNoun("dörr", Genus.Utrum)
    ];

    private bool isOpen = false;

    private bool isLocked = true;

    private bool hasKey = false;

    public override string Description => $"Källardörren är gjord i bastanta träplankor som tjärats för länge sedan, och den har ett nyckelhål under handtaget. Det växer ett tunt lager av grått mögel här och där. \nDörren är {(isOpen? "öppen" : "stängd")}. {(hasKey? "En järnnyckel sitter i nyckelhålet." :"")}";
    
    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (action.Predicate.Verb == Verbs.Öppna)
        {
            if (isOpen)
            {
                return new ActionResult()
                {
                    Message = "Källardörren är redan öppen."
                };
            }
            else if(isLocked)
            {
                return new ActionResult()
                {
                    Message = "Du trycker ner det rostiga handtaget, men dörren går inte att öppna. Den är låst."
                };
            }
            else
            {
                isOpen = true;
                return new ActionResult()
                {
                    Message = "Du öppnade dörren."
                };
            }
        }
        else if(action.Predicate.Verb == Verbs.Stäng && action.DirectObject == this)
        {
            if (isOpen)
            {
                isOpen = false;
                return new ActionResult()
                {
                    Message = "Dörren går igen med en dov duns"
                };
            }
            else
            {
                return new ActionResult()
                {
                    Message = "Det går inte att stänga dörren mer än den redan är"
                };
            }
        }
        else if (action.Predicate.Verb == Verbs.Lås_upp && action.DirectObject == this)
        {
            if(!isLocked)
            {
                return new ActionResult()
                {
                    Message = "Källardörren är redan upplåst."
                };
            }
            else if (action.MannerAdverbial != null)
            {
                if (action.MannerAdverbial.GetType() == typeof(KallarNyckel))
                {
                    if (context.Player.Inventory.Contains(action.MannerAdverbial))
                    {
                        isLocked = false;
                        hasKey = true;
                        context.Player.Inventory.Remove(action.MannerAdverbial);
                        return new ActionResult()
                        {
                            Message = "Du sätter nyckeln i låset och försöker förstå vilket håll nyckeln ska vridas åt. Det går trögt att vrida om nyckel, men när du väl lyckas med ett klagande gnissel och skrap så hörs ett ljudligt klick. Dörren är upplåst."
                        };
                    }
                    else
                    {
                        return new ActionResult()
                        {
                            Message = "Du måste ta upp nyckeln först."
                        };
                    }
                }
                else 
                {
                    return new ActionResult()
                    {
                        Message = $"Du kan inte låsa upp dörren med {action.MannerAdverbial.Name}"
                    };
                }
            }
            else if (hasKey)
            {
                isLocked = false;
                return new ActionResult()
                {
                    Message = "Det går trögt att vrida om nyckel som sitter i låset, men när du väl lyckas med ett klagande gnissel och skrap så hörs ett ljudligt klick. Dörren är upplåst."
                };
            }
            else
            {
                return new ActionResult()
                {
                    Message = $"Du måste använda en passande nyckel för att kunna låsa upp {Name.DefiniteForm}."
                };
            }
        }
        else if (action.Predicate.Verb == Verbs.Lås)
        {
            if(isLocked)
            {
                return new ActionResult()
                {
                    Message = "Källardörren är redan låst."
                };
            }
            else if (action.MannerAdverbial != null)
            {
                if (action.MannerAdverbial.GetType() == typeof(KallarNyckel))
                {
                    isLocked = true;
                    return new ActionResult()
                    {
                        Message = "Låset gnisslar och klickar till när du vrider nyckel. Dörren är nu låst igen."
                    };
                }
                else
                {
                    return new ActionResult()
                    {
                        Message = $"Du kan inte låsa dörren med {action.MannerAdverbial.Name}"
                    };
                }
            }
            else
            {
                if (!isOpen)
                {
                    if (hasKey)
                    {
                        return new ActionResult()
                        {
                            Message = "Låskolven fälls ut med ett metalliskt ljud. Dörren är låst."
                        };
                    }
                    else
                    {
                        return new ActionResult()
                        {
                            Message = "Du måste använda en passande nyckel för att kunna låsa dörren."
                        };
                    }
                }
                else
                {
                    return new ActionResult()
                    {
                        Message = "Är det inte lite udda att försöka låsa en öppen dörr?"
                    };
                }
            }
        }
        else if (action.Predicate.Verb == Verbs.Sätt && action.DirectObject is KallarNyckel && action.PlaceAdverbial == this && action.PlaceAdverbialInit == "i")
        {
            if (!hasKey)
            {
                if (context.Player.Inventory.Contains(action.DirectObject))
                {
                    hasKey = true;
                    return new ActionResult()
                    {
                        Message = "Du försöker sätta nyckeln i hålet under handtaget i dörren. Den passar, men du är tvungen att sätta in nyckeln upp och ned."
                    };
                }
                else
                {
                    return new ActionResult()
                    {
                        Message = "Du måste ta upp nyckeln först."
                    };
                }
            }
            else
            {
                return new ActionResult()
                {
                    Message = "Nyckeln sitter redan i låset i dörren."
                };
            }
        }
        else if (action.Predicate.Verb == Verbs.Ta && action.DirectObject is KallarNyckel && action.PlaceAdverbial == this && (action.PlaceAdverbialInit == "från" || action.PlaceAdverbialInit == "i"))
        {
            if (hasKey)
            {
                hasKey = false;
                context.Player.Inventory.Add(action.DirectObject);
                return new ActionResult()
                {
                    Message = "Du tar ut nyckeln ur nyckelhålet och stoppar den på dig."
                };
            }
            else
            {
                return new ActionResult()
                {
                    Message = "Det sitter ingen nyckel i dörren."
                };
            }
        }
        else
        {
            return null;
        }    
        
    }
}
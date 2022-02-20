using System.Collections.Generic;
using System.Linq;
using System;
namespace TestProject1
{
    internal class TestMethods
    {

        //--------------------------Punto 1-------------------------------------------
        internal static Stack<int> GetNextGreaterValue(Stack<int> sourceStack) 
        {
            Stack<int> result = new Stack<int>();
            int[] ArregloNums = sourceStack.ToArray();
            Array.Reverse(ArregloNums);
            int Count = sourceStack.Count;

            for (int i = 0; i <= Count - 1; i++)
            {
                if (i == (Count - 1))
                {
                    result.Push(-1);
                    break;
                }
                for (int j = i + 1; j < Count; j++)
                {
                    if (ArregloNums[i] < ArregloNums[j])
                    {
                        result.Push(ArregloNums[j]);
                        i++;
                    }

                    if (j == Count - 1)
                    {
                        if (ArregloNums[i] < ArregloNums[j])
                        {
                            result.Push(ArregloNums[j]);
                        }
                        else
                        {
                            result.Push(-1);
                        }
                    }
                }
            }                      
            return result;
        }
        //--------------------------Fin Punto 1-------------------------------------------

        //--------------------------Punto 2-------------------------------------------
        internal enum EValueType 
        {
            Two,
            Three,
            Five,
            Seven,
            Prime
        }
        internal static Dictionary<int, EValueType> FillDictionaryFromSource(int[] sourceArr) // Punto 2.A
        {
            Dictionary<int, EValueType> result = new Dictionary<int, EValueType>();
            for (int i = 0; i < sourceArr.Length; i++)
            {
                if (sourceArr[i] % 2 == 0)
                {
                    result.Add((int)sourceArr[i], EValueType.Two);
                }
                else if (sourceArr[i] % 3 == 0)
                {
                    result.Add((int)sourceArr[i], EValueType.Three);
                }
                else if (sourceArr[i] % 5 == 0)
                {
                    result.Add((int)sourceArr[i], EValueType.Five);
                }
                else if (sourceArr[i] % 7 == 0)
                {
                    result.Add((int)sourceArr[i], EValueType.Seven);
                }
                else 
                {
                    bool resultado = false;
                    int totalDivisores = 0;
                    for (int divisor = 1; divisor <= sourceArr[i]; divisor++)
                    {
                        if (sourceArr[i] % divisor == 0)
                            totalDivisores++;
                    }
                    if (totalDivisores == 2)
                        resultado = true;
                        result.Add((int)sourceArr[i], EValueType.Prime);
                }             
            }
            return result;
        }
        internal static int CountDictionaryRegistriesWithValueType(Dictionary<int, EValueType> sourceDict, EValueType type) //Punto 2.B
        {
            int Cant = 0;
            for (int i = 0; i < sourceDict.Count; i++)
            {               
                EValueType Valor = sourceDict.ElementAt(i).Value;
                if (Valor == type)
                {
                    Cant++;
                }
            }
            return Cant;
        }
      internal static Dictionary<int, EValueType> SortDictionaryRegistries(Dictionary<int, EValueType> sourceDict) //Punto 2.C
        {
            Dictionary<int, EValueType> result = new Dictionary<int, EValueType>();
            List<int> ListaLlaves = new List<int>();
            
            foreach (var llave in sourceDict.Keys) 
            {
                ListaLlaves.Add(llave);
            }
            int[] ArregloLLaves = ListaLlaves.ToArray();
            
            Array.Sort(ArregloLLaves);
            Array.Reverse(ArregloLLaves);

            result = FillDictionaryFromSource(ArregloLLaves);
            
            return result;                   
        }
        //--------------------------Fin Punto 2-------------------------------------------
           
        //--------------------------Punto 3-------------------------------------------
        internal static Queue<Ticket>[] ClassifyTickets(List<Ticket> sourceList) //Punto 3.A
        {
            Queue<Ticket>[] result = new Queue<Ticket>[3];                       
            Queue<Ticket> ColaPayment = new Queue<Ticket>();
            Queue<Ticket> ColaSubscription = new Queue<Ticket>();
            Queue<Ticket> ColaCancelations = new Queue<Ticket>();

            List<Ticket> ListaPayment = new List<Ticket>();
            List<Ticket> ListaSubscriptions = new List<Ticket>();
            List<Ticket> ListaCancellations = new List<Ticket>();

            for (int i = 0; i < sourceList.Count; i++)
            {
                if (sourceList[i].RequestType == Ticket.ERequestType.Payment)
                { 
                ListaPayment.Add(sourceList[i]);
                }
                if (sourceList[i].RequestType == Ticket.ERequestType.Subscription)
                {
                    ListaSubscriptions.Add(sourceList[i]);
                }
                if (sourceList[i].RequestType == Ticket.ERequestType.Cancellation)
                {
                    ListaCancellations.Add(sourceList[i]);
                }
            }

            List<Ticket> ListaPaymentsOrganizada = (ListaPayment.OrderBy(x => x.Turn).ToList());
            List<Ticket> ListaSubscriptionOrganizada = (ListaSubscriptions.OrderBy(x => x.Turn).ToList());
            List<Ticket> ListaCancellationsOrganizada = (ListaCancellations.OrderBy(x => x.Turn).ToList());

            for (int i = 0; i < ListaPaymentsOrganizada.Count; i++)
            {
                ColaPayment.Enqueue(ListaPaymentsOrganizada[i]);

            }
            for (int i = 0; i < ListaSubscriptionOrganizada.Count; i++)
            {
                ColaSubscription.Enqueue(ListaSubscriptionOrganizada[i]);
            }
            for (int i = 0; i < ListaCancellationsOrganizada.Count; i++)
            {
                ColaCancelations.Enqueue(ListaCancellationsOrganizada[i]);
            }

            result[0]=ColaPayment;
            result[1]=ColaSubscription;
            result[2]=ColaCancelations;

            return result;
        }      
        internal static bool AddNewTicket(Queue<Ticket> targetQueue, Ticket ticket) // Punto 3.B
        {
            bool result = false;           
            
            Ticket.ERequestType typeOfTicketQueue = targetQueue.Peek().RequestType;
            
            if (ticket.RequestType == typeOfTicketQueue && ticket.Turn > 0 && ticket.Turn < 100)
            {
                result = true;
            }
            return result;            
        }
        //--------------------------Fin Punto 3-------------------------------------------
    }
}
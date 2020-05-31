namespace _4sem_kurs_client
{
    public abstract class Selector
    {
        public abstract void Select(ref Selector selector, Manager manager, ref bool is_continue);
    }
}

using System;

namespace MedKarta.Shared.factoty
{
    public class AbstractFactoryView<T>
    {
        private readonly Func<T> factory;

        public AbstractFactoryView(Func<T> factory)
        {
            this.factory = factory;
        }

        public T Create()
        {
            return factory();
        }
    }
}
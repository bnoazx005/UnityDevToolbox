namespace UnityDevToolbox.Impls
{
    public struct Result<T, E>
    {
        private object mData;
        private readonly bool mHasError;

        public Result(T value)
        {
            mData = value;
            mHasError = false;
        }

        public Result(E errorValue)
        {
            mData = errorValue;
            mHasError = true;
        }

        public T GetOrDefault(T defaultValue = default(T))
        {
            if (mHasError)
            {
                return defaultValue;
            }

            return Value;
        }

        public static explicit operator bool(Result<T, E> result) => result.IsOk;

        public bool HasError => mHasError;
        public bool IsOk => !mHasError;

        public T Value
        {
            get
            {
                if (mHasError)
                {
                    throw new ResultBadAccessException();
                }

                return (T)mData;
            }
        }

        public E Error
        {
            get
            {
                if (!mHasError)
                {
                    throw new ResultErrorBadAccessException();
                }

                return (E)mData;
            }
        }
    }
}

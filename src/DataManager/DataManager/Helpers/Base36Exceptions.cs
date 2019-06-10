using System;

namespace XamarinEvolve.Utils.Base36
{
	public class InvalidBase36NumberException : Exception
	{
		public InvalidBase36NumberException()
		{
		}

		public InvalidBase36NumberException(string message) : base(message)
		{
		}

		public InvalidBase36NumberException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}

	public class InvalidBase36ValueException : Exception
	{
		public long NumericValue { get; }

		public InvalidBase36ValueException()
		{
		}

		public InvalidBase36ValueException(string message) : base(message)
		{
		}

		public InvalidBase36ValueException(long numericValue)
		{
			this.NumericValue = numericValue;
		}

		public InvalidBase36ValueException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}

	public class InvalidBase36DigitValueException : Exception
	{
		public byte NumericValue { get; }

		public InvalidBase36DigitValueException()
		{
		}

		public InvalidBase36DigitValueException(string message) : base(message)
		{
		}

		public InvalidBase36DigitValueException(byte numericValue)
		{
			this.NumericValue = numericValue;
		}

		public InvalidBase36DigitValueException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}

	public class InvalidBase36DigitException : Exception
	{
		public char Base36Digit { get; }

		public InvalidBase36DigitException()
		{
		}

		public InvalidBase36DigitException(string message) : base(message)
		{
		}

		public InvalidBase36DigitException(char base36Digit)
		{
			this.Base36Digit = base36Digit;
		}

		public InvalidBase36DigitException(string message, Exception innerException) : base(message, innerException)
		{
		}

	}
}
using System;

// For when things have truly gone south
public class OhNoException : Exception {
	public OhNoException(string message) : base(message) {
	}
}

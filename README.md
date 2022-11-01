# RSA algorithm C#


# Description
 
The acronym RSA comes from the names of Ron Rivest, Adi Shamir and Leonard Adleman. These are the authors who described the algorithm publicly in 1977. The RSA algorithm is an asymmetric cryptographic algorithm. Asymmetric means that it works on two different keys, i.e. a public key and a private key, which are mathematically related to each other:
- *public key* - the key used to encrypt a given message,
- *private key* - the key used to decrypt a message, which is inaccessible to others.

The user of the RSA algorithm creates a public key based on two large prime numbers and some auxiliary number. This key is publicly available and anyone can use it to encrypt a message. However, decryption is only possible with the use of the private key by someone who is in possession of it.

The RSA algorithm is very difficult to break. Its security is based on the difficulty of factorizing the product of two large prime numbers. Multiplying the two numbers is easy, but determining the original prime numbers from the sum (factorization) is considered unfeasible. This problem is known as the RSA problem. The algorithm is relatively slow in operation, and as a result, it is not as widely used.
 
# How it works?

## Generate the keys:

1. Select two large prime numbers *x* and *y*.
2. Calculate $n = x * y$
3. Calculate the *totient* function $\phi(n) = (x-1)(y-1)$ 

# Launch

TODO

# Sources

TODO

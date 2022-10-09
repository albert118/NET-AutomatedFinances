const MAX_KEYS = 100;

const generate = () => Math.floor(Math.random() * MAX_KEYS);

/**
 * Generate a pseudo-random integer key on the range [0, 100]
 * @returns pseudo-random key
 */
const pseudoKeyGenerator = () => {
    let nextIndex = 0;

    const iterator = {
        next() {
            if (nextIndex < MAX_KEYS - 1) {
                nextIndex++;
                return { value: generate(), done: false };
            }

            return { value: generate(), done: true };
        }
    };

    return iterator
};

export default pseudoKeyGenerator;
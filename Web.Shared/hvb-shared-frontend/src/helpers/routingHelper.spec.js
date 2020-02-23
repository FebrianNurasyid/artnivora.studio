import {
    isOnPath
} from './routingHelper';

describe('IsOnPath', () => {
    it('matches when an exact path is the same', () => {
        const currentPath = '/this/is/a/path_example';
        const targetPath = '/this/is/a/path_example';

        expect(isOnPath(currentPath, targetPath, true)).toEqual(true);
    });

    it('does not match when an exact path is not the same', () => {
        const currentPath = '/this/is/a/slah_example';
        const targetPath = '/this/is/a/path_example';

        expect(isOnPath(currentPath, targetPath, true)).toEqual(false);
    });

    it('does match when a path is not exactly the same and matchExactly is set to false', () => {
        const currentPath = '/this/is/a/path_example';
        const targetPath = '/this/';

        expect(isOnPath(currentPath, targetPath, false)).toEqual(true);
    });

    it('does not match when a path is completely different and matchExactly is set to false', () => {
        const currentPath = 'something';
        const targetPath = '/this/is/a/path_example';

        expect(isOnPath(currentPath, targetPath, false)).toEqual(false);
    });
});
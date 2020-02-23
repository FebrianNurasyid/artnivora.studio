export const isOnPath = (currentPath, targetPath, matchExactly) => {
    if (matchExactly) {
        return currentPath === targetPath;
    }
    return currentPath.indexOf(targetPath) !== -1;
}
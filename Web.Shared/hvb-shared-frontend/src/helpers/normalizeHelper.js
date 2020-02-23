import { Map, fromJS } from 'immutable';

export const normalize = (original, key) => {
    return fromJS(original)
        .reduce((newMap, item) => {
            return newMap.set(item.get(key), item);
        }, Map());
}

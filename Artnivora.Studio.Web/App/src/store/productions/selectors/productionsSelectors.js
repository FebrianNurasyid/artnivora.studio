import { createSelector } from 'reselect';

export const productionsStateSelector = state => state.productions;

export const productionsSelector = state => productionsStateSelector(state).productions;

export const filteredProductionsSelector = createSelector(
    productionsSelector,
    (productions) => {
        return productions;
    }
);

export const filteredAllProductionsSelector = createSelector(
    productionsSelector,
    (productions) => {
        return productions;
    }
);
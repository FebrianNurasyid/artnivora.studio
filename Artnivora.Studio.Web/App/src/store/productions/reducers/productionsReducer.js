import {
    uploadAttachmentProductionType,
    fetchProductionsType,
} from '../../constants';

import { Map } from 'immutable';

const initialState = { productions: [], production: [], isLoading: false };

export default (state, action) => {
    state = state || initialState;

    if (action.type === uploadAttachmentProductionType) {
        return {
            ...state,
            attachmentProduction: action.attachmentProduction
        }
    }

    if (action.type === fetchProductionsType) {
        return {
            ...state,
            productions: action.productions,
            isLoading: false
        };
    }

    return state;
};

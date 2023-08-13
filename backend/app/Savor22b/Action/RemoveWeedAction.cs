namespace Savor22b.Action;

using System.Collections.Immutable;
using Bencodex.Types;
using Libplanet.Action;
using Libplanet.State;
using Libplanet.Headless.Extensions;
using Savor22b.States;
using Savor22b.Action.Exceptions;

[ActionType(nameof(RemoveWeedAction))]
public class RemoveWeedAction : SVRAction
{
    public int FieldIndex { get; private set; }

    public RemoveWeedAction()
    {
    }

    public RemoveWeedAction(int fieldIndex)
    {
        FieldIndex = fieldIndex;
    }

    protected override IImmutableDictionary<string, IValue> PlainValueInternal =>
        new Dictionary<string, IValue>()
        {
            [(Text)"fieldIndex"] = FieldIndex.Serialize(),
        }.ToImmutableDictionary();

    protected override void LoadPlainValueInternal(
        IImmutableDictionary<string, IValue> plainValue)
    {
        FieldIndex = plainValue[(Text)"fieldIndex"].ToInteger();
    }


    public override IAccountStateDelta Execute(IActionContext ctx)
    {

        IAccountStateDelta states = ctx.PreviousStates;
        RootState rootState = states.GetState(ctx.Signer) is Bencodex.Types.Dictionary rootStateEncoded
            ? new RootState(rootStateEncoded)
            : new RootState();

        VillageState? villageState = rootState.VillageState;

        if (villageState is null) {
            throw new InvalidVillageStateException("VillageState is null");
        }

        HouseFieldState houseFieldState = villageState.HouseFieldStates[FieldIndex] ?? throw new InvalidFieldIndexException("FieldIndex is invalid");


        if (houseFieldState.IsHarvestable(ctx.BlockIndex))
        {
            throw new HarvestableFieldException("Field is harvestable");
        }

        if (houseFieldState.WeedRemovalAble(ctx.BlockIndex) is false)
        {
            throw new WeedRemovalAbleException("WeedRemoval is not able");
        }

        houseFieldState.RemovalWeed(ctx.BlockIndex);

        villageState.UpdateHouseFieldState(FieldIndex, houseFieldState);
        rootState.SetVillageState(villageState);

        var aa = rootState.Serialize();

        states = states.SetState(ctx.Signer, aa);

        return states;
    }
}

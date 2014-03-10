function UseItmeTrigGer(Player, ItemId, Slot, Count)
	if ItemId==1000000101 then
		UpdateHpAndMp(Player, 70, 0)
		RemoveItem(Player, Slot, 1)
	elseif ItemId==1000000104 then
		RemoveItem(Player, Slot, 1)
		UpdateHpAndMp(Player, 0, 70)
	end
end